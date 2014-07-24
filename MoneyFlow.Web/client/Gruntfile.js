module.exports = function(grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        clean: ['js/*.js'],

        concat: {
            options: {
                banner: '/* <%= pkg.name %> v.<%= pkg.version %> */\n\n'
            },
            data: {
                src: ['src/data/app.js', 'src/data/*.js'],
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
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('default', ['clean', 'concat'])
};