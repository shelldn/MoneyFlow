module.exports = function(grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        less: {
            compile: {
                files: {
                    'css/style.css': 'css/style.less'
                }
            }
        },

        clean: {
            src     : ['src/**/*.coffee.js'],
            test    : ['test/**/*.coffee.js'],
            dest    : ['js/*.js']
        },

        coffee: {
            options: {
                bare: true
            },
            compile: {
                files: [{
                    expand: true,
                    src: ['src/**/*.coffee'],
                    ext: '.coffee.js'
                }]
            }
        },

        concat: {
            options: {
                banner: '/* <%= pkg.name %> v.<%= pkg.version %> */\n\n'
            },
            data: {
                src: ['src/data/app.js', 'src/data/**/*.js'],
                dest: 'js/data.js'
            },
            controls: {
                src: ['src/controls/app.js', 'src/controls/**/*.js'],
                dest: 'js/controls.js'
            },
            auth: {
                src: ['src/auth/app.js', 'src/auth/*.js'],
                dest: 'js/auth.js'
            },
            tracker: {
                src: ['src/tracker/app.js', 'src/tracker/*.js'],
                dest: 'js/tracker.js'
            }
        },

        karma: {
            test: {
                configFile: 'karma.conf.js',
                background: true,
                singleRun: false
            }
        },

        watch: {
            src: {
                files: ['src/**/*', '!src/**/*.coffee.js'],
                tasks: ['default']
            },
            karma: {
                files: ['src/**/*', 'test/**/*Spec.js'],
                tasks: ['default', 'karma:test:run']
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-coffee');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-karma');

    grunt.registerTask('default', ['clean:dest', 'coffee', 'concat', 'clean:src']);
    grunt.registerTask('develop', ['karma:test:start', 'watch:karma']);
};