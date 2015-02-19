describe('directive: mf-expand', function() {
    var $0, $scope;

    beforeEach(module('mf.controls'));

    // loading templates
    beforeEach(module('mf.tmpl'));

    beforeEach(inject(function($rootScope, $compile) {
        $0 = '<div mf-expand="Expand me">Contents</div>';
        $0 = $compile($0)($rootScope);

        $rootScope.$digest();

        $scope = $0.isolateScope();
    }));

    it('Should provide a link with the text of mf-expand attribute', function() {
        expect($0.find('a:first-child').html()).toBe('Expand me');
    });
    
    it('Should transclude the element\'s contents', function() {
        expect($0.children('div.expand-content').html()).toBe('<span class="ng-scope">Contents</span>');
    }); 

    it('Should toggle isExpanded when tglExpand() is called', function() {
        $scope.isExpanded = false;

        $scope.tglExpand();
        expect($scope.isExpanded).toBeTruthy();

        $scope.tglExpand();
        expect($scope.isExpanded).toBeFalsy();
    });
});