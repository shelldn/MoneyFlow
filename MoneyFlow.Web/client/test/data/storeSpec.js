describe('store-for: Cost (remote)', function() {
    var $httpBackend, costStore,
        Cost;

    var URL_COSTS           = '/api/costs',
        URL_COSTS_PERIODS   = '/api/costs/periods';

    beforeEach(module('mf.data'));

    beforeEach(inject(function(_$httpBackend_, _remoteCostStore_, _Cost_) {
        $httpBackend = _$httpBackend_;
        costStore = _remoteCostStore_;
        Cost = _Cost_;

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

        $httpBackend.whenGET(URL_COSTS_PERIODS).respond(periods);
        $httpBackend.expectGET(URL_COSTS_PERIODS);

        // act
        var ret = costStore
            .getPeriods();

        $httpBackend.flush();

        // assert
        expect(ret.length).toEqual(periods.length);
        expect(ret[0]).toEqual(periods[0]);
        expect(ret[1]).toEqual(periods[1]);
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
        expect(ret.length).toEqual(1);
        expect(ret[0]).toEqual('2014-07-13T14:00:00');
    });

    it('should create cost and return its server represented instance', function() {
        var cost = new Cost(25, {}, '2014-07-13T14:00:00'),
            serverCost = angular.copy(cost);

        serverCost.id = 1;

        var fakeSuccess = jasmine.createSpy('success');

        $httpBackend.whenPOST(URL_COSTS, cost).respond(200, serverCost);

        // act
        costStore.create(cost).then(fakeSuccess);
        $httpBackend.flush();

        // assert
        expect(fakeSuccess).toHaveBeenCalledWith(
            jasmine.objectContaining(serverCost)
        );
    });
});