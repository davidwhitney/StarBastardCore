
function player(playerNumber, playerName){
    var $playerNumber = playerNumber;
    var $playerName = playerName;

    var $resources = new playerResources();

    this.playerNumber = function(){
        return $playerNumber;
    };
    this.playerName = function(){
        return $playerName;
    };

    this.resources = function(){
        return $resources;
    };

    this.modifyResources = function(resourceModification){
        $resources.addFood(resourceModification.food())
        $resources.addOre(resourceModification.ore())
        $resources.addTech(resourceModification.tech())
    };

}