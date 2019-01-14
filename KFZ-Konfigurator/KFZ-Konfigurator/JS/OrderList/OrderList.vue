<template>
    <div>
        <h2 class="mt-3">{{ $t('orderListView.header') }}</h2>

        <!-- Paging -->
        <!--<nav class="d-flex justify-content-end" v-show="pages.length >= 1">
            <ul class="pagination" v-for="item in pages">
                <li class="page-item" v-bind:class="{active: currentPageIndex === (item - 1)}">
                    <a class="page-link" href="#" @@click="loadPage(item)">{{item}}</a>
                </li>
            </ul>
        </nav>-->

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

            <tbody v-for="item in orders" :key="item.id">
                <tr>
                    <td>{{ item.Model }}</td>
                    <td>{{ item.Description }}</td>
                    <td>{{ item.DateTime }}</td>
                    <td>{{ $n(item.BasePrice, 'currency') }}</td>
                    <td>{{ $n(item.ExtrasPrice, 'currency') }}</td>
                    <td>{{ $n(item.BasePrice + item.ExtrasPrice, 'currency') }}</td>
                    <td>
                        <a v-bind:href="item.linkUrl">{{ $t('general.view') }}</a> |
                        <a href="#" data-target="#confirmCancelDialog" data-toggle="modal" v-bind:data-id="item.id">{{ $t('general.cancel') }}</a>
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- Confirm Cancel Modal -->
        <!--<div class="modal fade" id="confirmCancelDialog" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">@LocalizationManager.Localize("OrderListView_CancelConfirm_Title")</h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <span>&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>@LocalizationManager.Localize("OrderListView_CancelConfirm_Text")</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">@LocalizationManager.Localize("General_No")</button>
                        <button type="button" class="btn btn-primary" id="confirmCancelBtn" data-dismiss="modal"
                                @@click="deleteItem($event.target.dataset.orderid)">
                            @LocalizationManager.Localize("General_Yes")
                        </button>
                    </div>
                </div>
            </div>
        </div>-->
    </div>
</template>

<script>
    import { getOrderList } from '../Api/data.js';
    import { times } from 'lodash';

    export default {
        data() {
            return {
                orders: [],
                pages: [],
                currentPageIndex: 0
            };
        },
        created() {
            getOrderList().then(data => {
                this.orders = data.Orders;
                times(data.PageCount, (index) => this.pages.push(index + 1));
            });
        }
    }
</script>