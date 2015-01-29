angular.module 'mf.controls'

  #
  # Expand

  .directive 'mfExpand', () ->
    transclude: true
    scope:
      caption: '@mfExpand'

    link: (scope, element, attr, ctrl, transclude) ->
      content = element.find '.expand-content';

      scope.tglExpand = () ->
        scope.isExpanded = !scope.isExpanded;
        return;

      transclude scope.$parent, (clone) ->
        content.append clone;

      return;

    templateUrl: "/tmpl/controls/expand"