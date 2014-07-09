function playerResources(){
    var $food = 0;
    var $ore = 0;
    var $tech = 0;

    this.food = function(){
        return $food;
    };
    this.ore = function(){
        return $ore;
    };
    this.tech = function(){
        return $tech;
    };

    this.addFood = function(amount){
        $food += amount;
    };
    this.addOre = function(amount){
        $ore += amount;
    };
    this.addTech = function(amount){
        $tech += amount;
    };
}