module.exports = function(grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        clean: {
            src: ['src/**/*.coffee.js'],
            dest: ['js/*.js']
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
            tracker: {
                src: ['src/tracker/app.js', 'src/tracker/*.js'],
                dest: 'js/tracker.js'
            }
        },

        watch: {
            src: {
                files: ['src/**/*.js'],
                tasks: ['default']
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-coffee');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default', ['clean:dest', 'coffee', 'concat', 'clean:src']);
};