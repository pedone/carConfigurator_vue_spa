﻿const path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const LodashModuleReplacementPlugin = require('lodash-webpack-plugin');

const bundleOutputDir = './JS/dist';

const config = {
    entry: {
        main: './JS/main.index.js',
        orderList: './JS/orderList.index.js'
    },
    //watch: true,
    output: {
        path: path.resolve(__dirname, bundleOutputDir),
        filename: '[name].bundle.js'
    },
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    plugins: ['lodash']
                }
            },
            {
                test: /\.js$/,
                loader: 'babel-loader'
            }
        ]
    },
    resolve: {
        //alias: {
        //    'vue$': 'vue/dist/vue.esm.js'
        //},
        extensions: [
            '.js',
            '.vue'
        ]
    },
    plugins: [
        new VueLoaderPlugin(),
        new LodashModuleReplacementPlugin
    ]
}

module.exports = config;