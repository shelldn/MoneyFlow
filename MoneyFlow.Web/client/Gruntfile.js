module.exports = function(grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            options: {
                banner: '/* <%= pkg.name %> v.<%= pkg.version %> */\n\n'
            },
            data: {
                src: ['src/data/app.js', 'src/data/*.js'],
                dest: 'js/data.js'
            },
            components: {
                src: ['src/components/app.js', 'src/components/**/*.js'],
                dest: 'js/components.js'
            },
            tracker: {
                src: ['src/tracker/app.js', 'src/tracker/*.js'],
                dest: 'js/tracker.js'
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('default', ['concat'])
};