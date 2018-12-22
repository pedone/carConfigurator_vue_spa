'use strict';

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var ConfigurationOverviewViewModel =
/** @param {HandlerOptions} options */
function ConfigurationOverviewViewModel(options) {
  _classCallCheck(this, ConfigurationOverviewViewModel);

  var self = this;
  /** @type {string} */

  this.configurationDescription = ko.observable('');
  /** @type {HandlerOptions} */

  this.options = options;

  this.placeOrder = function () {
    $.ajax({
      type: 'POST',
      url: '/ConfigurationOverview/PlaceOrder',
      data: {
        __RequestVerificationToken: self.options.antiForgeryToken,
        description: self.configurationDescription()
      },
      contentType: 'application/x-www-form-urlencoded',
      dataType: 'text'
    }).done(function (data) {
      console.debug('order successfully placed');

      if (self.options.placeOrderSuccess) {
        self.options.placeOrderSuccess(data);
      }
    }).fail(function (error) {
      console.error('failed to place order: ' + error.responseText + ' (' + error.statusText + ')');
      console.debug(JSON.stringify(error));

      if (self.options.placeOrderFailure) {
        self.options.placeOrderFailure();
      }
    });
  };
};
/**
 * @typedef {Object} HandlerOptions
 * @property {string} antiForgeryToken
 * @property {Function|null} placeOrderSuccess
 * @property {Function|null} placeOrderFailure
 */
'use strict';

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var ViewModel =
/** @param {ViewModelData|NameViewModelData|AccessoryViewModelData|RimViewModelData} data */
function ViewModel(data) {
  _classCallCheck(this, ViewModel);

  /** @type {number} */
  this.id = data.Id;
  /** @type {number} */

  this.price = data.Price;
  /** @type {boolean} */

  this.isSelected = ko.observable(data.IsSelected);
  /** @type {number|null} */

  this.name = data.Name;
  /** @type {number|null} */

  this.size = data.Size;
  /** @type {number|null} */

  this.category = data.Category;
};
/**
 * @property {Object.<ViewModel>} _engineSettingsById
 * @property {Object.<ViewModel>} _rimsById
 * @property {Object.<ViewModel>} _paintById
 * @property {Object.<ViewModel>} accessoriesById
 * @property {Array.<ViewModel>} selectedAccessories
 */


var ConfigurationViewModel =
/*#__PURE__*/
function () {
  /**
   * @param {ConfigurationData} data
   */
  function ConfigurationViewModel(data) {
    var _this = this;

    _classCallCheck(this, ConfigurationViewModel);

    Object.defineProperties(this, {
      _engineSettingsById: {
        value: this._toViewModelDictionary(data.EngineSettings),
        writable: false,
        enumerable: false
      },
      _rimsById: {
        value: this._toViewModelDictionary(data.Rims),
        writable: false,
        enumerable: false
      },
      _paintById: {
        value: this._toViewModelDictionary(data.Paints),
        writable: false,
        enumerable: false
      },
      accessoriesById: {
        value: this._toViewModelDictionary(data.Accessories),
        writable: false,
        enumerable: true
      }
    });
    /**
     * koObservable
     * @type {string}
     */

    this.selectedPaintId = ko.observable(this._getInitialSelectedId(data.Paints));
    /** @type {string} */

    this._initialPaintId = this.selectedPaintId();
    /**
     * koObservable
     * @type {string}
     */

    this.selectedRimsId = ko.observable(this._getInitialSelectedId(data.Rims));
    /** @type {string} */

    this._initialRimsId = this.selectedRimsId();
    /**
     * koObservable
     * @type {Array.<ViewModel>}
     */

    this.selectedAccessories = ko.observableArray(_.values(this.accessoriesById)).extend({
      filterSelected: null
    });
    /** @type {Array.<number>} */

    this._initialAccessoryIds = this.selectedAccessoryIds;
    /**
     * koComputed
     * @type {ViewModel}
     */

    this.selectedEngineSettings = ko.computed(function () {
      var values = _.values(_this._engineSettingsById); // only one setting is supposed to be selected


      return _.first(_.filter(values, function (cur) {
        return cur.isSelected();
      }));
    });
    /** @type {ViewModel} */

    this._initialEngineSettings = this.selectedEngineSettings();
    /**
     * koComputed
     * @type {ViewModel}
     */

    this.selectedPaint = ko.computed(function () {
      return _this._paintById[_this.selectedPaintId()];
    });
    /**
     * koComputed
     * @type {ViewModel}
     */

    this.selectedRims = ko.computed(function () {
      return _this._rimsById[_this.selectedRimsId()];
    });
    /**
     * Calculates the combined price of everything but the engine
     * koComputed
     * @type {number}
     */

    this.extrasPrice = ko.computed(function () {
      /** @type {number} */
      var accessoriesPrice = _.reduce(_this.selectedAccessories(), function (memo, cur) {
        return memo + cur.price;
      }, 0);

      return accessoriesPrice + (_this.selectedPaint() && _this.selectedPaint().price) + (_this.selectedRims() && _this.selectedRims().price);
    });
    /**
     * Just the engine price
     * koComputed
     * @type {number}
     */

    this.basePrice = ko.computed(function () {
      return _this.selectedEngineSettings() && _this.selectedEngineSettings().price || 0;
    });
    /**
     * The base price combined with the extras price
     * koComputed
     * @type {number}
     */

    this.fullPrice = ko.computed(function () {
      return _this.basePrice() + _this.extrasPrice();
    });
  }
  /**
   * @param {Array.<ViewModelData>} items
   * @returns {string}
   * @private
   */


  _createClass(ConfigurationViewModel, [{
    key: "_getInitialSelectedId",
    value: function _getInitialSelectedId(items) {
      if (!items || items.length === 0) {
        return -1;
      }
      /** @type {ViewModelData} */


      var selectedItem = _.find(items, function (cur) {
        return cur.IsSelected;
      }) || items[0];
      return selectedItem.Id.toString();
    }
    /**
     * @param {Array.<ViewModelData>} items
     * @returns {Object.<ViewModel>}
     * @private
     */

  }, {
    key: "_toViewModelDictionary",
    value: function _toViewModelDictionary(items) {
      /** @type {Object.<ViewModel>} */
      var result = {};

      _.each(items, function (cur) {
        result[cur.Id] = new ViewModel(cur);
      });

      return result;
    }
    /** @returns {Array.<number>} */

  }, {
    key: "countCategory",

    /**
     * @param {Array.<ViewModel>} accessories
     * @param {string} category
     */
    value: function countCategory(accessories, category) {
      return _.filter(accessories, function (cur) {
        return cur.category === category;
      }).length;
    }
    /** @param {number} settingsId */

  }, {
    key: "selectEngineSettings",
    value: function selectEngineSettings(settingsId) {
      //deselect all other settings, because deselection doesn't work with binding
      _.each(this._engineSettingsById, function (cur) {
        cur.isSelected(cur.id === settingsId);
      });
    }
    /** @param {string} id */

  }, {
    key: "selectPaint",
    value: function selectPaint(id) {
      this.selectedPaintId(id);
    }
    /** @param {string} id */

  }, {
    key: "selectAccessory",
    value: function selectAccessory(id) {
      /** @type {boolean} */
      var isSelected = this.accessoriesById[id].isSelected();
      this.accessoriesById[id].isSelected(!isSelected);
    }
    /** @param {string} antiForgeryToken */

  }, {
    key: "saveChanges",
    value: function saveChanges(antiForgeryToken) {
      var selectedAccessoryIds = this.selectedAccessoryIds; //check for changes

      var accessoriesChanged = this._initialAccessoryIds.length !== selectedAccessoryIds.length || _.difference(this._initialAccessoryIds, this.selectedAccessoryIds).length > 0;
      var engineSettingsChanged = this._initialEngineSettings != this.selectedEngineSettings();
      var paintChanged = this._initialPaintId != this.selectedPaintId();
      var rimsChanged = this._initialRimsId != this.selectedRimsId();

      if (!accessoriesChanged && !engineSettingsChanged && !paintChanged && !rimsChanged) {
        console.debug('no configuration changes');
        return;
      } // package changes


      var changedData = {
        __RequestVerificationToken: antiForgeryToken
      };

      if (paintChanged) {
        changedData.paintId = viewModel.selectedPaintId();
      }

      if (rimsChanged) {
        changedData.rimId = viewModel.selectedRimsId();
      }

      if (engineSettingsChanged) {
        changedData.engineSettingsId = viewModel.selectedEngineSettings().id;
      }

      if (accessoriesChanged) {
        changedData.accessoryIds = selectedAccessoryIds;
      }

      console.debug('saving configuration changes'); //send changes

      $.ajax({
        //make sure the changes are saved before the next page is loaded
        async: false,
        type: 'POST',
        url: '/Configuration/UpdateActiveConfiguration',
        data: changedData,
        contentType: 'application/x-www-form-urlencoded'
      }).fail(function (error) {
        console.error('failed to save configuration changes: ' + error.responseText + ' (' + error.statusText + ')');
        console.debug(JSON.stringify(error)); //alert('something went wrong. see console for details');
      });
    }
  }, {
    key: "selectedAccessoryIds",
    get: function get() {
      return _.map(this.selectedAccessories(), function (cur) {
        return cur.id;
      });
    }
  }]);

  return ConfigurationViewModel;
}();
/**
 * @typedef {Object} ViewModelData
 * @property {number} Id
 * @property {number} Price
 * @property {boolean} IsSelected
 */

/**
 * @typedef {ViewModelData} NameViewModelData
 * @property {string} Name
 */

/**
 * @typedef {ViewModelData} RimViewModelData
 * @property {number} Size
 */

/**
 * @typedef {NameViewModelData} AccessoryViewModelData
 * @property {number} Category
 */

/**
 * @typedef {Object} ConfigurationData
 * @property {Array.<ViewModelData>} EngineSettings
 * @property {Array.<ViewModelData>} Accessories
 * @property {Array.<ViewModelData>} Paints
 * @property {Array.<ViewModelData>} Rims
 */
'use strict';
/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */

function saveViewModel(url, id, antiForgeryToken, additionalData) {
  var i;
  /** @type {Object} */

  var dataObject = {
    __RequestVerificationToken: antiForgeryToken
  };

  if (id) {
    dataObject.id = id;
  }

  if (additionalData) {
    for (i in additionalData) {
      if (additionalData.hasOwnProperty(i)) {
        dataObject[i] = additionalData[i];
      }
    }
  }

  return $.ajax({
    type: 'POST',
    url: url,
    data: dataObject,
    contentType: 'application/x-www-form-urlencoded'
  });
}
/**
 * @param {Document} document
 * @returns {string}
 */


function getAntiForgeryToken(document) {
  return document.find('[name="__RequestVerificationToken"]').val();
}

function formatCurrency(amount) {
  return amount.toLocaleString() + ' EUR';
}
'use strict';
/** @param {KnockoutObservableArrayStatic<ViewModel>} target */

ko.extenders.filterSelected = function (target) {
  /** @type {number} */
  var i;
  /** @type {Array.<ViewModel>} */

  var data = target();
  /** @type {KnockoutObservableArrayStatic<ViewModel>} */

  var result = ko.observableArray(_.filter(data, function (cur) {
    return cur.isSelected();
  }));
  /** @type {Function} */

  var subscribeFactory = function subscribeFactory(index) {
    return function (value) {
      if (value) {
        result.push(data[index]);
      } else {
        result.remove(data[index]);
      }
    };
  };

  for (i = 0; i < data.length; i += 1) {
    //capture the current index with a function factory
    data[i].isSelected.subscribe(subscribeFactory(i));
  }

  return result;
};
'use strict';

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

var ModelSelectionViewModel =
/*#__PURE__*/
function () {
  function ModelSelectionViewModel() {
    _classCallCheck(this, ModelSelectionViewModel);

    /**
     * koObservable
     * @type {string}
     */
    this.curModelFilter = ko.observable('');
  }
  /**
   * @param {string} filter
   */


  _createClass(ModelSelectionViewModel, [{
    key: "filterModels",
    value: function filterModels(filter) {
      if (filter !== this.curModelFilter()) {
        this.curModelFilter(filter);
      }
    }
  }]);

  return ModelSelectionViewModel;
}();
'use strict';

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var OrderItemViewModel =
/** @param {OrderData} data */
function OrderItemViewModel(data) {
  _classCallCheck(this, OrderItemViewModel);

  /** @type {number} */
  this.id = data.Id;
  /** @type {number} */

  this.basePrice = data.BasePrice;
  /** @type {number} */

  this.extrasPrice = data.ExtrasPrice;
  /** @type {number} */

  this.price = data.Price;
  /** @type {string} */

  this.description = data.Description;
  /** @type {string} */

  this.dateTime = data.DateTime;
  /** @type {string} */

  this.model = data.Model;
  /** @type {string} */

  this.guid = data.Guid;
  /** @type {string} */

  this.linkUrl = data.LinkUrl;
};

var OrderListViewModel =
/**
 * @param {Array.<OrderData>} data
 * @param {number} pageCount
 */
function OrderListViewModel(data, pageCount) {
  var _this = this;

  _classCallCheck(this, OrderListViewModel);

  var self = this;
  /** @type {KnockoutObservableArrayStatic} */

  this.orders = ko.observableArray(_.map(data, function (cur) {
    return new OrderItemViewModel(cur);
  }));
  /**
   * koObservableArray
   * @type {Array.<number>}
   */

  this.pages = ko.observableArray([]);

  _.times(pageCount, function (index) {
    return _this.pages().push(index + 1);
  });
  /**
   * koObservable
   * @type {number}
   */


  this.currentPageIndex = ko.observable(0);
  /**
   * Notes: this method has to be placed within the constructor, because self.orders is not returning the
   * KnockoutObservableArrayStatic object outside the constructor, but the function that knockout is generating.
   * @param {number} itemId
   * @param {JQueryStatic} document
   */

  this.deleteItem = function (itemId, document) {
    var item = _.find(self.orders(), function (cur) {
      return cur.id == itemId;
    });

    if (!item) {
      console.error('order ' + itemId + ' not found');
      return;
    }
    /** @type {string} */


    var antiForgeryToken = getAntiForgeryToken(document);
    deleteItemAjax(itemId, antiForgeryToken).done(
    /** @param {{NewPageCount: number, NewItem: OrderData }} data */
    function (data) {
      console.debug('removing order ' + itemId + ' from view');
      self.orders.remove(item);

      if (data.NewItem) {
        //insert the item that has moved up to the current page
        self.orders.push(new OrderItemViewModel(data.NewItem));
      }

      if (self.pages().length !== data.NewPageCount) {
        console.debug('page count changed from ' + self.pages().length + ' to ' + data.NewPageCount); //update the max page count

        self.pages.pop(); // if the current page is larger than the max pages, gotta load the previous page

        /** @type {number} */

        var curPageNumber = self.currentPageIndex() + 1;

        if (curPageNumber > self.pages().length) {
          self.loadPage(curPageNumber - 1);
        }
      }
    }).fail(function (error) {
      console.error('failed to delete order: ' + error.responseText + ' (' + error.statusText + ')');
      console.debug(JSON.stringify(error));
      alert('order ' + item.name + ' could not be removed');
    });
  };
  /**
   * @param {number} id
   * @param {string} antiForgeryToken
   * @returns {jqXHR}
   */


  function deleteItemAjax(id, antiForgeryToken) {
    return $.ajax({
      type: 'POST',
      url: '/OrderList/delete',
      data: {
        __RequestVerificationToken: antiForgeryToken,
        id: id,
        pageIndex: self.currentPageIndex()
      },
      contentType: 'application/x-www-form-urlencoded',
      dataType: 'json'
    });
  }
  /** @param {number} number */


  this.loadPage = function (number) {
    /** @type {number} */
    var targetIndex = number - 1;

    if (targetIndex === self.currentPageIndex()) {
      return;
    }

    $.ajax({
      //make sure the data is saved before the next page is loaded
      async: false,
      type: 'GET',
      url: '/OrderList/LoadPage',
      data: {
        pageIndex: targetIndex
      },
      dataType: 'json'
    }).done(function (data) {
      console.debug('order list for page index ' + targetIndex + ' successfully retrieved');
      self.currentPageIndex(targetIndex);
      self.orders(_.map(data, function (cur) {
        return new OrderItemViewModel(cur);
      }));
    }).fail(function (error) {
      console.error('failed to load page: ' + error.responseText + ' (' + error.statusText + ')');
      console.debug(JSON.stringify(error));
    });
  };
};
/**
 * @typedef {Object} OrderData
 * @property {number} Id
 * @property {number} ExtrasPrice
 * @property {number} BasePrice
 * @property {number} Price
 * @property {string} Description
 * @property {string} DateTime
 * @property {string} Model
 * @property {string} Guid
 * @property {string} LinkUrl
 */