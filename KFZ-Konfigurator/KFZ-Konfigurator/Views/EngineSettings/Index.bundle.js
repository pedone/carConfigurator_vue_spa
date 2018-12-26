(function(f){if(typeof exports==="object"&&typeof module!=="undefined"){module.exports=f()}else if(typeof define==="function"&&define.amd){define([],f)}else{var g;if(typeof window!=="undefined"){g=window}else if(typeof global!=="undefined"){g=global}else if(typeof self!=="undefined"){g=self}else{g=this}g.myBundle = f()}})(function(){var define,module,exports;return (function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.buildVue = buildVue;

var helper = _interopRequireWildcard(require("./Helper.js"));

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } else { var newObj = {}; if (obj != null) { for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) { var desc = Object.defineProperty && Object.getOwnPropertyDescriptor ? Object.getOwnPropertyDescriptor(obj, key) : {}; if (desc.get || desc.set) { Object.defineProperty(newObj, key, desc); } else { newObj[key] = obj[key]; } } } } newObj.default = obj; return newObj; } }

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

/**
 * @param {ViewModelData|NameViewModelData|AccessoryViewModelData|RimViewModelData} data
 * @returns {ViewModel}
 */


function buildViewModel(data) {
  /** @type {ViewModel} */
  return {
    id: data.Id,
    category: data.Category,
    isSelected: data.IsSelected,
    name: data.Name,
    price: data.Price,
    size: data.Size
  };
}
/**
 * @param {Array.<ViewModelData>} items
 * @returns {Object.<ViewModel>}
 * @private
 */


function toViewModelDictionary(items) {
  /** @type {Object.<ViewModel>} */
  var result = {};

  _.each(items, function (cur) {
    result[cur.Id] = buildViewModel(cur);
  });

  return result;
}
/**
 * @param {Array.<ViewModelData>} items
 * @returns {string}
 * @private
 */


function getInitialSelectedId(items) {
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
 * @param {ConfigurationData} data
 */


function buildVue(data) {
  return new Vue({
    el: '#app',
    data: {
      selectedPaintId: getInitialSelectedId(data.Paints),
      selectedRimsId: getInitialSelectedId(data.Rims),
      engineSettingsById: toViewModelDictionary(data.EngineSettings),
      accessoriesById: toViewModelDictionary(data.Accessories),
      accesoryCountByCategory: {}
    },
    created: function created() {
      this._getSelectedAccessoryIds = function () {
        return _.map(this.selectedAccessories, function (cur) {
          return cur.id;
        });
      };

      this._paintById = toViewModelDictionary(data.Paints);
      this._rimsById = toViewModelDictionary(data.Rims);
      this._initialAccessoryIds = this._getSelectedAccessoryIds();
      this._initialEngineSettings = this.selectedEngineSettings;
      this._initialPaintId = this.selectedPaintId;
      this._initialRimsId = this.selectedRimsId;
    },
    computed: {
      /** @type {ViewModel} */
      selectedPaint: function selectedPaint() {
        return this._paintById[this.selectedPaintId];
      },

      /** @type {ViewModel} */
      selectedRims: function selectedRims() {
        return this._rimsById[this.selectedRimsId];
      },

      /** @type {Array.<ViewModel>} */
      selectedAccessories: function selectedAccessories() {
        /** @type {Array.<ViewModel>} */
        var values = _.values(this.accessoriesById);

        return _.filter(values, function (cur) {
          return cur.isSelected;
        });
      },

      /**
       * Just the engine price
       * @type {number}
       */
      basePrice: function basePrice() {
        return this.selectedEngineSettings && this.selectedEngineSettings.price || 0;
      },
      formattedBasePrice: function formattedBasePrice() {
        return helper.formatCurrency(this.basePrice);
      },

      /**
       * Calculates the combined price of everything but the engine
       * @type {number}
       */
      extrasPrice: function extrasPrice() {
        /** @type {number} */
        var accessoriesPrice = _.reduce(this.selectedAccessories, function (memo, cur) {
          return memo + cur.price;
        }, 0);

        return accessoriesPrice + (this.selectedPaint && this.selectedPaint.price) + (this.selectedRims && this.selectedRims.price);
      },
      formattedExtrasPrice: function formattedExtrasPrice() {
        return helper.formatCurrency(this.extrasPrice);
      },

      /**
       * The base price combined with the extras price
       * @type {number}
       */
      formattedFullPrice: function formattedFullPrice() {
        return this.basePrice + this.extrasPrice;
      },

      /** @type {ViewModel} */
      selectedEngineSettings: function selectedEngineSettings() {
        var values = _.values(this.engineSettingsById); // only one setting is supposed to be selected


        return _.first(_.filter(values, function (cur) {
          return cur.isSelected;
        }));
      }
    },
    watch: {
      selectedAccessories: function selectedAccessories(items) {
        this.accesoryCountByCategory = {};
        var _iteratorNormalCompletion = true;
        var _didIteratorError = false;
        var _iteratorError = undefined;

        try {
          for (var _iterator = items[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
            var cur = _step.value;
            this.accesoryCountByCategory[cur.category] = (this.accesoryCountByCategory[cur.category] || 0) + 1;
          }
        } catch (err) {
          _didIteratorError = true;
          _iteratorError = err;
        } finally {
          try {
            if (!_iteratorNormalCompletion && _iterator.return != null) {
              _iterator.return();
            }
          } finally {
            if (_didIteratorError) {
              throw _iteratorError;
            }
          }
        }
      }
    },
    methods: {
      /** @param {string} id */
      toggleAccessorySelection: function toggleAccessorySelection(id) {
        this.accessoriesById[id].isSelected = !this.accessoriesById[id].isSelected;
      },

      /** @param {number} settingsId */
      selectEngineSettings: function selectEngineSettings(settingsId) {
        //deselect all other settings, because deselection doesn't work with binding
        _.each(this.engineSettingsById, function (cur) {
          cur.isSelected = cur.id === settingsId;
        });
      },

      /** @param {string} antiForgeryToken */
      saveChanges: function saveChanges(antiForgeryToken) {
        /** @type {Array.<number>} */
        var selectedAccessoryIds = this._getSelectedAccessoryIds(); //check for changes


        var accessoriesChanged = this._initialAccessoryIds.length !== selectedAccessoryIds.length || _.difference(this._initialAccessoryIds, selectedAccessoryIds).length > 0;
        var engineSettingsChanged = this._initialEngineSettings != this.selectedEngineSettings;
        var paintChanged = this._initialPaintId != this.selectedPaintId;
        var rimsChanged = this._initialRimsId != this.selectedRimsId;

        if (!accessoriesChanged && !engineSettingsChanged && !paintChanged && !rimsChanged) {
          console.debug('no configuration changes');
          return;
        } // package changes


        var changedData = {
          __RequestVerificationToken: antiForgeryToken
        };

        if (paintChanged) {
          changedData.paintId = this.selectedPaintId;
        }

        if (rimsChanged) {
          changedData.rimId = this.selectedRimsId;
        }

        if (engineSettingsChanged) {
          changedData.engineSettingsId = this.selectedEngineSettings.id;
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
    }
  });
}
/**
 * @typedef {Object} ViewModel
 * @property {number} id
 * @property {number} price
 * @property {boolean} isSelected
 * @property {string} name
 * @property {number|null} size
 * @property {number|null} category
 */

},{"./Helper.js":2}],2:[function(require,module,exports){
'use strict';
/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.saveViewModel = saveViewModel;
exports.getAntiForgeryToken = getAntiForgeryToken;
exports.formatCurrency = formatCurrency;

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

},{}],3:[function(require,module,exports){
'use strict';
/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.saveViewModel = saveViewModel;
exports.getAntiForgeryToken = getAntiForgeryToken;
exports.formatCurrency = formatCurrency;

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

},{}],4:[function(require,module,exports){
"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.helper = exports.configurationViewModel = void 0;

var configurationViewModel = _interopRequireWildcard(require("../../Scripts/app/ConfigurationViewModel.js"));

exports.configurationViewModel = configurationViewModel;

var helper = _interopRequireWildcard(require("../../Scripts/app/helper.js"));

exports.helper = helper;

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } else { var newObj = {}; if (obj != null) { for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) { var desc = Object.defineProperty && Object.getOwnPropertyDescriptor ? Object.getOwnPropertyDescriptor(obj, key) : {}; if (desc.get || desc.set) { Object.defineProperty(newObj, key, desc); } else { newObj[key] = obj[key]; } } } } newObj.default = obj; return newObj; } }

},{"../../Scripts/app/ConfigurationViewModel.js":1,"../../Scripts/app/helper.js":3}]},{},[4])(4)
});
