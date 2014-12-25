describe('store-for: Cost', function() {
    var $httpBackend, costStore;

    beforeEach(module('mf.data'));

    beforeEach(inject(function(_$httpBackend_, _costStore_) {
        $httpBackend = _$httpBackend_;
        costStore = _costStore_;

        $httpBackend.when('GET', '/api/costs/2014-07').respond(['2014-07-13T14:00:00']);
    }));

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it('should load all the costs for specified period', function() {
        var period = '2014-07-01T00:00:00';

        // act
        var ret = costStore.getByPeriod(period);
        $httpBackend.flush();

        // assert
        expect(ret).toEqual(['2014-07-13T14:00:00']);
    });
});