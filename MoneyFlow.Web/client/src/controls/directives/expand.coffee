angular.module 'mf.controls'

  #
  # Expand

  .directive 'mfExpand', () ->
    transclude: true
    scope:
      caption: '@mfExpand'

    link: (scope, $0, attr, ctrl, transclude) ->
      content = $0.find '.expand-content';

      scope.tglExpand = () ->
        scope.isExpanded = !scope.isExpanded;
        return;

      transclude scope.$parent, (clone) ->
        content.append clone;

      return;

    templateUrl: "/tmpl/controls/expand"