describe('store-for: Cost', function() {
    var $httpBackend, costStore;

    var URL_PERIODS = '/api/costs/periods';

    beforeEach(module('mf.data'));

    beforeEach(inject(function(_$httpBackend_, _costStore_) {
        $httpBackend = _$httpBackend_;
        costStore = _costStore_;

        $httpBackend.when('GET', '/api/costs/2014-07')
            .respond(['2014-07-13T14:00:00']);
    }));

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it('should fetch and return all the periods for costs', function() {
        var periods = [
            '2014-01-01T00:00:00',
            '2014-12-01T00:00:00'
        ];

        $httpBackend.whenGET(URL_PERIODS).respond(periods);
        $httpBackend.expectGET(URL_PERIODS);

        // act
        var result = costStore
            .getPeriods();

        $httpBackend.flush();

        // assert
        expect(result).toEqual(periods);
    });

    it('should translate period to year-month pair', function() {
        var pair = '2014-07',
            period = pair + '-01T00:00:00';

        $httpBackend.expectGET('/api/costs/' + pair);

        // act
        costStore.getByPeriod(period);
        $httpBackend.flush();
    }); 
    
    it('should fetch and return all the costs for specified period', function() {
        var period = '2014-07-01T00:00:00';

        // act
        var ret = costStore.getByPeriod(period);
        $httpBackend.flush();

        // assert
        expect(ret).toEqual(['2014-07-13T14:00:00']);
    });
});