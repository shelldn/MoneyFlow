describe('directive: mf-expand', function() {
    var x, $scope;

    beforeEach(module('mf.controls'));

    beforeEach(inject(function($rootScope, $compile) {
        $scope = $rootScope.$new();

        x = '<div mf-expand="Expand me">Contents</div>';
        x = $compile(x)($scope);

        $scope.$digest();
    }));

    it('Should provide a link with the text of mf-expand attribute', function() {
        expect(x.find('a:first-child').html()).toBe('Expand me');
    });
    
    it('Should transclude the element\'s contents', function() {
        expect(x.children('div[ng-transclude]').html()).toBe('<span class="ng-scope">Contents</span>');
    }); 

    it('Should toggle isExpanded when tglExpand() is called', function() {
        $scope.isExpanded = false;

        $scope.tglExpand();
        expect($scope.isExpanded).toBeTruthy();

        $scope.tglExpand();
        expect($scope.isExpanded).toBeFalsy();
    });
});