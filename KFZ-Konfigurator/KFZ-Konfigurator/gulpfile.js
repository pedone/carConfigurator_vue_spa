//inspired by http://www.lambdatwist.com/dot-net-vuejs

'use strict';

const { src, dest, series } = require('gulp'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    minify = require('gulp-uglify'),
    babel = require('gulp-babel'),
    less = require('gulp-less'),
    browserify = require('browserify'),
    aliasify = require('aliasify'),
    sourcemaps = require('gulp-sourcemaps'),
    rename = require('gulp-rename'),
    source = require('vinyl-source-stream'),
    watchify = require('watchify'),
    buffer = require('vinyl-buffer'),
    glob = require('glob'),
    path = require('path'),
    through2 = require('through2');


const aliasifyConfig = {
    aliases: {
        'vue$': 'vue/dist/vue.js',
        'underscore$': 'lodash/lodash.js'
    },
    verbose: true
}
const paths = {
    scriptFiles: './ViewModels/**/main.js',
    scriptOutDir: './Scripts/app/',
    scriptResources: './Scripts/app/'
};

/**
 * @param {string} pattern
 * @returns {Array.<string>}
 */
function getFilesOrFolders(pattern) {
    return glob.sync(pattern);
}

/**
 * @param {string} input
 * @param {string} output
 */
function compileJS(input, output) {
    var b = browserify({
        debug: true,
        entries: [input],
        basedir: process.env.INIT_CWD,
        paths: [paths.scriptResources]
    });

    return b.transform(aliasify, aliasifyConfig)
        .transform('babelify', { presets: ["@babel/preset-env"] })
        .bundle()
        .pipe(source('bundle.js'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(minify())
        //.on('error', gutil.log)
        //.pipe(sourcemaps.write('./'))
        .pipe(dest(output));
}

function watchJSFolder(input, output) {
    var b = browserify({
        entries: [input],
        plugin: [watchify],
        //cache and packageCache neccessary for watchify
        cache: {},
        packageCache: {},
        basedir: process.env.INIT_CWD,
        paths: [paths.scriptResources]
    });

    function bundle() {
        b.transform(aliasify, aliasifyConfig)
            //.transform('babelify', { presets: ["@babel/preset-env"] })
            .bundle()
            .pipe(source('bundle.js'))
            .pipe(buffer())
            .pipe(sourcemaps.init({ loadMaps: true }))
            // Add transformation tasks to the pipeline here.
            //.pipe(minify())
            //  .on('error', gutil.log)
            .pipe(sourcemaps.write('./'))
            .pipe(dest(output));

        console.log(new Date().toLocaleTimeString() + ': JS Bundle rebuilt: ' + output);
    }
    b.on('update', bundle);
    bundle();
}

exports.build_JS_Production = function (done) {
    let files = getFilesOrFolders(paths.scriptFiles);
    console.log(files);

    files.map(function (cur) {
        let directory = path.dirname(cur);
        let dirName = directory.substring(directory.lastIndexOf('/') + 1);
        compileJS(cur, paths.scriptOutDir + dirName);
    });
    done();
};

exports.watch_JS_Develop = function (done) {
    let files = getFilesOrFolders(paths.scriptFiles);
    console.log(files);

    files.map(function (cur) {
        let directory = path.dirname(cur);
        let dirName = directory.substring(directory.lastIndexOf('/') + 1);
        watchJSFolder(cur, paths.scriptOutDir + dirName);
    });
    done();
};

//const contentPath = './Content';
//const contentAppPath = './Content/app';
//const scriptPath = './Scripts';
//const scriptAppPath = './Scripts/app';
//const paths = {
//    less: {
//        src: contentAppPath + '/**/*.less',
//        destDir: contentPath,
//    },
//    css: {
//        src: [contentAppPath + '/**/*.css', '!' + contentAppPath + '/**/*.min.css'],
//        dest: contentPath + '/out/site.css',
//        minExtension: '.min.css'
//    },
//    babel: {
//        src: [scriptAppPath + '/**/*.js', '!' + scriptAppPath + '/**/*.min.js'],
//        dest: scriptPath + '/out/site.js',
//        minExtension: '.min.js'
//    }
//};

//function logFileName(name) {
//    console.log(name);
//    return through2.obj(function (file, _, cb) {
//        console.log(file.history);
//        cb(null, file);
//    });
//}

//// LESS

//function compileLess() {
//    return src(paths.less.src)
//        .pipe(less())
//        .pipe(rename({ extension: '.css' }))
//        .pipe(dest(paths.less.destDir));
//}

//// CSS

//function cleanCSS(done) {
//    rimraf(paths.css.dest, done);
//}

//function compileCSS() {
//    return src(paths.css.src)
//        .pipe(logFileName('minCSS'))
//        .pipe(concat(paths.css.dest))
//        .pipe(dest('.'))
//        .pipe(cssmin())
//        .pipe(rename({ extname: paths.css.minExtension }))
//        .pipe(dest('.'));
//}

//// JS

////function cleanJS(done) {
////    rimraf(paths.babel.dest, done);
////}

////function bundleMinJS() {
////    return src(paths.babel.src)
////        .pipe(babel({ presets: ['@babel/env'] }))
////        .pipe(logFileName('bundleMinJS'))
////        .pipe(concat(paths.babel.dest))
////        .pipe(dest('.'))
////        .pipe(uglify())
////        .pipe(rename({ extname: paths.babel.minExtension }))
////        .pipe(dest('.'));
////}

//// Bundle JS in Views
//function bundleViewJs(done) {
//    glob('./Views/**/*.js', { ignore: ['./**/*.bundle.js', './**/*.bundle.min.js'] }, function (er, files) {
//        files.forEach(curFile => {
//            browserify(curFile, { standalone: 'myBundle' })
//                .transform('babelify', { presets: ["@babel/preset-env"] })
//                .bundle()
//                .pipe(source(curFile))
//                .pipe(buffer())
//                .pipe(logFileName('bundleViewJs.' + curFile))
//                .pipe(rename({ extname: '.bundle.js' }))
//                .pipe(dest('.'))
//                .pipe(uglify())
//                .pipe(rename({ extname: '.min.js' }))
//                .pipe(dest('.'));
//        });
//        done();
//    });
//}

//exports.bundleViewJs = bundleViewJs;
//exports.cleanCSS = cleanCSS;
//exports.compileCSS = series(cleanCSS, compileLess, compileCSS);

//exports.compileLess = compileLess;
////exports.cleanJS = cleanJS;
////exports.compileJS = series(cleanJS, bundleMinJS);