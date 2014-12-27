window.CostStoreMock = (function() {

    var Mock = function() {};

    Mock.GetByPeriodResult = function(period) {
        this.period = period;
    };

    Mock.prototype.getPeriods = function() {

    };

    Mock.prototype.getByPeriod = function(period) {
        return new Mock.GetByPeriodResult(period);
    };

    return Mock;

})();