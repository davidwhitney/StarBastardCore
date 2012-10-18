function constructionStarship(ownerId){
    var $movement = 1;
    var $playerOwner = ownerId;

    this.name = function(){
        return "Construction Ship";
    };

    this.movement = function(){
        return $movement;
    }

    this.owner = function(){
        return $playerOwner;
    }

    this.canBuild = function(){
        return true;
    }
}