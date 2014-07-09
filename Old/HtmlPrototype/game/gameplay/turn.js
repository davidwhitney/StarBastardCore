function turn(player){
    var $player = player;
    var $actionQueue = new Array();

    this.tick = function(context){
        var playerResourceChanges = new Array();

        $.each(context.systems(), function(key, value) {
            if(value.owner() == $player.playerNumber()){

                var planetResourceChanges = value.progressOneTurn();
                $.each(planetResourceChanges, function(key, value) {
                    playerResourceChanges.push(value);
                });
            }
        });

        $.each(playerResourceChanges, function(key, value) {
            $player.modifyResources(value);
        });
    };

    this.actionQueue = function(){
        return $actionQueue;
    };

    this.commitTurn = function(){
        $.each($actionQueue, function(key, value) {
            value.execute();
        });

    };
}