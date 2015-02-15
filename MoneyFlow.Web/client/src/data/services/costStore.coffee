angular.module 'mf.data'

  #
  # CostStore factory
  # (remote or local)

  .factory 'costStore', ($injector) -> $injector.get 'remoteCostStore'