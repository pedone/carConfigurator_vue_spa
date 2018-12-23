(function(f){if(typeof exports==="object"&&typeof module!=="undefined"){module.exports=f()}else if(typeof define==="function"&&define.amd){define([],f)}else{var g;if(typeof window!=="undefined"){g=window}else if(typeof global!=="undefined"){g=global}else if(typeof self!=="undefined"){g=self}else{g=this}g.myBundle = f()}})(function(){var define,module,exports;return (function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
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

},{}],2:[function(require,module,exports){
"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.helper = void 0;

var helper = _interopRequireWildcard(require("../../Scripts/app/helper.js"));

exports.helper = helper;

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } else { var newObj = {}; if (obj != null) { for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) { var desc = Object.defineProperty && Object.getOwnPropertyDescriptor ? Object.getOwnPropertyDescriptor(obj, key) : {}; if (desc.get || desc.set) { Object.defineProperty(newObj, key, desc); } else { newObj[key] = obj[key]; } } } } newObj.default = obj; return newObj; } }

},{"../../Scripts/app/helper.js":1}]},{},[2])(2)
});
