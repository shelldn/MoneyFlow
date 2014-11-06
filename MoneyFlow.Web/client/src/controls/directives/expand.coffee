angular.module 'mf.controls'

  #
  # Expand

  .directive 'mfExpand', () ->
    transclude: true
    scope: true
    link: (scope, _, attrs) ->
      scope.cap = attrs['mfExpand'];

      scope.tglExpand = () ->
        scope.isExpanded = !scope.isExpanded;
        return;
      return;

    templateUrl: "/tmpl/controls/expand"