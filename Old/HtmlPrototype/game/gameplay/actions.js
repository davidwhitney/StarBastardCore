function buildAction(building, targetSystem, payingPlayer){
    var $building = building;
    var $targetSystem = targetSystem;
    var $payingPlayer = payingPlayer

    this.execute = function(){
        $targetSystem.build($payingPlayer, $building);
    };

    this.summary = function(){
        return "Building " + $building.name() + " on " + $targetSystem.name();
    };
}