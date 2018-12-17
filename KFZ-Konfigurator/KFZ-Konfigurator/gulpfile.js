'use strict';

const { src, dest, series } = require('gulp'),
    rimraf = require('rimraf'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify'),
    babel = require('gulp-babel'),
    less = require('gulp-less'),
    rename = require('gulp-rename');

const paths = {
    less: {
        src: './Content/**/*.less',
        destDir: './Content',
    },
    css: {
        src: ['./Content/**/*.css', '!./Content/**/*.min.css'],
        dest: './Content/out/site.min.css'
    },
    babel: {
        src: ['./Scripts/**/*.js', '!./Scripts/**/*.min.js'],
        dest: './Scripts/out/site.min.js'
    }
};

// LESS

function compileLess() {
    return src(paths.less.src)
        .pipe(less())
        .pipe(rename({ extension: '.css' }))
        .pipe(dest(paths.less.destDir));
}

// CSS

function cleanCSS(done) {
    rimraf(paths.css.dest, done);
}

function minCSS() {
    return src(paths.css.src)
        .pipe(concat(paths.css.dest))
        .pipe(cssmin())
        .pipe(dest('.'));
}

// JS

function cleanJS(done) {
    rimraf(paths.babel.dest, done);
}

function bundleMinJS() {
    return src(paths.babel.src)
        .pipe(babel({ presets: ['@babel/env'] }))
        .pipe(concat(paths.babel.dest))
        .pipe(uglify())
        .pipe(dest('.'));
}

exports.cleanCSS = cleanCSS;
exports.minCSS = series(compileLess, minCSS);

exports.compileLess = compileLess;
exports.cleanJS = cleanJS;
exports.bundleJS = series(cleanJS, bundleMinJS);