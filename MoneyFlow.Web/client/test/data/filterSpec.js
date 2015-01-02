describe('filter: link', function() {
    var link, Cost;

    beforeEach(module('mf.data'));

    beforeEach(inject(function($filter, _Cost_) {
        link = $filter('link');
        Cost = _Cost_;
    }));

    it("it should link items with 'prev' and 'next' properties in defined order", function() {
        var costs = [
            new Cost(10, {}, '2014-07-10T14:00:00'),
            new Cost(10, {}, '2014-07-20T14:00:00'),
            new Cost(10, {}, '2014-07-30T14:00:00')
        ];

        // act
        costs = link(costs);

        // assert
        expect(costs[0]).toEqual(jasmine.objectContaining({ prev: undefined,    next: costs[1]  }));
        expect(costs[1]).toEqual(jasmine.objectContaining({ prev: costs[0],     next: costs[2]  }));
        expect(costs[2]).toEqual(jasmine.objectContaining({ prev: costs[1],     next: undefined }));
    });

    it("should treat sequence as first-is-a-last if 'reverse' flag is set", function() {
        var items = [
            // next: undefined
            { item: 1 },
            { item: 0 }
            // prev: undefined
        ];

        // act
        items = link(items, true);

        // assert
        expect(items[0]).toEqual(jasmine.objectContaining({ next: undefined,    prev: items[1]  }));
        expect(items[1]).toEqual(jasmine.objectContaining({ next: items[0],     prev: undefined }));
    });
});