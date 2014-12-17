// Karma configuration
// Generated on Mon Oct 27 2014 01:50:54 GMT+0200 (FLE Standard Time)

module.exports = function(config) {
  config.set({

    frameworks: ['jasmine'],

    preprocessors: {
      "../**/*.cshtml": ['ng-html2js']
    },

    // list of files / patterns to load in the browser
    files: [

      // vendor
      'js/vendor/jquery/dist/jquery.js',
      'js/vendor/angular/angular.js',
      'js/vendor/angular-mocks/angular-mocks.js',

      // templates
      '../tmpl/**/*.cshtml',

      // app
      'js/*.js',
      'test/**/*Spec.js'
    ],

    ngHtml2JsPreprocessor: {
      stripPrefix: '.*/MoneyFlow.Web',
      stripSufix: '.cshtml',
      moduleName: 'mf.tmpl'
    },

    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    browsers: ['PhantomJS'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false
  });
};
