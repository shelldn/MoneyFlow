describe('directive: mf-expand', function() {
    var x, $scope;

    beforeEach(module('mf.controls'));

    // loading templates
    beforeEach(module('mf.tmpl'));

    beforeEach(inject(function($rootScope, $compile) {
        x = '<div mf-expand="Expand me">Contents</div>';
        x = $compile(x)($rootScope);

        $scope = x.scope();

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