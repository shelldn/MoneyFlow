// Karma configuration
// Generated on Mon Oct 27 2014 01:50:54 GMT+0200 (FLE Standard Time)

module.exports = function(config) {
  config.set({

    frameworks: ['jasmine'],

    autoWatch: false,

    preprocessors: {
      "../**/*.cshtml": ['ng-html2js']
    },

    // list of files / patterns to load in the browser
    files: [

      // vendor
      'js/vendor/moment/moment.js',
      'js/vendor/jquery/dist/jquery.js',
      'js/vendor/angular/angular.js',
      'js/vendor/angular-mocks/angular-mocks.js',
      'js/vendor/angular-animate/angular-animate.js',
      'js/vendor/angular-resource/angular-resource.js',
      'js/vendor/angular-messages/angular-messages.js',
      'js/vendor/angular-local-storage/dist/angular-local-storage.js',

      // templates
      '../tmpl/**/*.cshtml',

      // app
      'js/*.js',
      'test/_mocks/*.js',
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
    port: 8765,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // start these browsers
    browsers: ['PhantomJS']
  });
};
