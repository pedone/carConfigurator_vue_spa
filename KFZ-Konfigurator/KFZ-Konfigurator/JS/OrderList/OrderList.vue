<template>
    <div>
        <h2 class="mt-3">{{ $t('orderListView.header') }}</h2>

        <!-- Paging -->
        <nav class="d-flex justify-content-end" v-show="pages.length >= 1">
            <ul class="pagination" v-for="item in pages">
                <li class="page-item" v-bind:class="{ active: currentPageIndex === (item - 1) }">
                    <a class="page-link" href="#" @click="loadPage(item)">{{ item }}</a>
                </li>
            </ul>
        </nav>

        <table class="table">
            <tr>
                <th>
                    {{ $t('orderListView.carModelHeader') }}
                </th>
                <th>
                    {{ $t('orderListView.descriptionHeader') }}
                </th>
                <th>
                    {{ $t('orderListView.dateTimeHeader') }}
                </th>
                <th>
                    {{ $t('orderListView.basePriceHeader') }}
                </th>
                <th>
                    {{ $t('orderListView.extrasPriceHeader') }}
                </th>
                <th>
                    {{ $t('orderListView.priceHeader') }}
                </th>
                <th></th>
            </tr>

            <tbody v-for="item in orders" :key="item.Id">
                <tr>
                    <td>{{ item.Model }}</td>
                    <td>{{ item.Description }}</td>
                    <td>{{ item.DateTime }}</td>
                    <td>{{ $n(item.BasePrice, 'currency') }}</td>
                    <td>{{ $n(item.ExtrasPrice, 'currency') }}</td>
                    <td>{{ $n(item.BasePrice + item.ExtrasPrice, 'currency') }}</td>
                    <td>
                        <router-link v-bind:to="{ name: $store.state.constants.routes.orderOverview, params: { guid: item.Guid } }">{{ $t('general.view') }}</router-link> |
                        <a href="#" data-target="#confirmCancelDialog" data-toggle="modal" v-bind:data-id="item.Id">{{ $t('general.cancel') }}</a>
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- Confirm Cancel Modal -->
        <div class="modal fade" id="confirmCancelDialog" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ $t('orderListView.cancelConfirmTitle') }}</h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <span>&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>{{ $t('orderListView.cancelConfirmText') }}</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">{{ $t('general.no') }}</button>
                        <button type="button" class="btn btn-primary" id="confirmCancelBtn" data-dismiss="modal"
                                @click="deleteItem($event.target.dataset.orderid)">
                            {{ $t('general.yes') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { getOrderList } from '../Api/data.js';
    import { times, find } from 'lodash';

    export default {
        data() {
            return {
                orders: [],
                pages: [],
                currentPageIndex: 0
            };
        },
        inject: ['antiForgeryToken'],
        created() {
            let self = this;

            getOrderList().then(data => {
                this.orders = data.Orders;
                times(data.PageCount, (index) => this.pages.push(index + 1));
            });

            /**
             * @param {number} id
             * @param {string} antiForgeryToken
             * @returns {jqXHR}
             */
            this._deleteItemAjax = function (id) {
                return $.ajax({
                    type: 'POST',
                    url: '/Data/deleteOrder',
                    data: {
                        __RequestVerificationToken: this.antiForgeryToken,
                        id: id,
                        pageIndex: self.currentPageIndex
                    },
                    contentType: 'application/x-www-form-urlencoded',
                    dataType: 'json'
                });
            };

            $(document).on('show.bs.modal', '#confirmCancelDialog', function (e) {
                let orderId = $(e.relatedTarget).data('id');
                $(this).find('#confirmCancelBtn').attr('data-orderid', orderId);
            });
        },
        methods: {
            /** @param {number} itemId */
            deleteItem(itemId) {
                let self = this;
                var item = find(this.orders, (cur) => cur.Id == itemId);
                if (!item) {
                    console.error('order ' + itemId + ' not found');
                    return;
                }

                this._deleteItemAjax(itemId, this._antiForgeryToken)
                    .done(
                        /** @param {{NewPageCount: number, NewItem: OrderData }} data */
                        function (data) {
                            console.debug('removing order ' + itemId + ' from view');
                            self.orders.splice(self.orders.indexOf(item), 1);
                            if (data.NewItem) {
                                //insert the item that has moved up to the current page
                                self.orders.push(data.NewItem);
                            }
                            if (self.pages.length !== data.NewPageCount) {
                                console.debug('page count changed from ' + self.pages.length + ' to ' + data.NewPageCount);
                                //update the max page count
                                self.pages.pop();

                                // if the current page is larger than the max pages, gotta load the previous page
                                /** @type {number} */
                                let curPageNumber = self.currentPageIndex + 1;
                                if (curPageNumber > self.pages.length) {
                                    self.loadPage(curPageNumber - 1);
                                }
                            }
                        })
                    .fail(function (error) {
                        console.error('failed to delete order: ' + error.responseText + ' (' + error.statusText + ')');
                        console.debug(JSON.stringify(error));
                        alert('order ' + item.name + ' could not be removed');
                    });
            },
            /** @param {number} number */
            loadPage(number) {
                let self = this;
                /** @type {number} */
                let targetIndex = number - 1;
                if (targetIndex === this.currentPageIndex) {
                    return;
                }

                $.ajax({
                    //make sure the data is saved before the next page is loaded
                    async: false,
                    type: 'GET',
                    url: '/Data/LoadOrderPage',
                    data: { pageIndex: targetIndex },
                    dataType: 'json'
                }).done(function (data) {
                    console.debug('order list for page index ' + targetIndex + ' successfully retrieved');
                    self.currentPageIndex = targetIndex;
                    self.orders = data;
                }).fail(function (error) {
                    console.error('failed to load page: ' + error.responseText + ' (' + error.statusText + ')');
                    console.debug(JSON.stringify(error));
                });
            }
        }
    }
</script>