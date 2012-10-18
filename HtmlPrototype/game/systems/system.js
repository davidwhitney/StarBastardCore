function system(name, systemNumber, buildingCap, owner) {
    var $name = name;
    var $systemNumber = systemNumber;
    var $buildingCap = buildingCap;
    var $owner = owner;

    var $orbit = new Array();
    var $battlefield = new Array();
    var $city = new Array();

    this.name = function(){
        return name;
    };

    this.owner = function(){
        return $owner;
    };

    this.systemNumber = function(){
        return $systemNumber;
    };

    this.buildingCap = function(){
        return $buildingCap;
    };

    this.city = function(){
      return $city;
    };

    this.battlefield = function(){
      return $battlefield;
    };

    this.orbit = function(){
      return $orbit;
    };

    this.playerShipsInOrbit = function(){
        var ships = new Array();
        $.each($orbit, function(key, value) {
            if(value.owner() == $owner) {
                ships.push(value);
            }
        });
        return ships;
    };

    this.enemyShipsInOrbit = function(){
        var ships = new Array();
        $.each($orbit, function(key, value) {
            if(value.owner() != $owner) {
                ships.push(value);
            }
        });
        return ships;
    };

    this.build = function(payingPlayer, building) {
        if (!this.canBuild()) {
            alert('I cannae build!');
            return;
        }

        var cost = building.constructionCost();
        payingPlayer.resources().addOre((cost.ore()*-1))
        payingPlayer.resources().addFood((cost.food()*-1))
        payingPlayer.resources().addTech((cost.tech()*-1))
        $city.push(building);

    };

    this.resourceIncreasePrediction = function(){
        var resourceChange = new playerResources();
        $.each($city, function(key, value) {
            var changeThisUnitProduces = value.produce();
            resourceChange.addFood(changeThisUnitProduces.food());
            resourceChange.addOre(changeThisUnitProduces.ore());
            resourceChange.addTech(changeThisUnitProduces.tech());
        });
        return resourceChange;
    };

    this.canBuild = function(){
        if($buildingCap <= $city.length){
            return false;
        }

        if(this.playerShipsInOrbit().length == 0){
            return false;
        }

        var foundConstructionShip = false;
        $.each(this.playerShipsInOrbit(), function(key, value) {
            if(value.canBuild()){
                foundConstructionShip = true;
            }
        });

        if(!foundConstructionShip){
            return false;
        }

        return true;
    };


    this.progressOneTurn = function(){
        var playerResourceChanges = new Array();

        $.each($city, function(key, value) {
            playerResourceChanges.push(value.produce());
        });

        return playerResourceChanges;
    };
}