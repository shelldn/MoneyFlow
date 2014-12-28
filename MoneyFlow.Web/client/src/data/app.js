angular.module('mf.data', ['ngResource'])

    // Types

    .constant('Cost', function(amount, category, date) {
        this.amount = amount;
        this.category = category;
        this.date = date;

        Object.defineProperty(this, 'period', {
            enumerable: false,
            get: function() {
                return moment(this.date)
                    .format('YYYY-MM-01T00:00:00');
            }
        });
    });