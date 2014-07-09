function Mine(){

    this.name = function(){
        return "Mine";
    };

    this.produce = function(){
        var resourceMod = new playerResources();
        resourceMod.addOre(4);
        return resourceMod;
    };

    this.constructionCost = function(){
        var resourceMod = new playerResources();
        resourceMod.addOre(4);
        return resourceMod;
    }
};

function Farm(){

    this.name = function(){
        return "Farm";
    };

    this.produce = function(){
        var resourceMod = new playerResources();
        resourceMod.addFood(4);
        return resourceMod;
    };

    this.constructionCost = function(){
        var resourceMod = new playerResources();
        resourceMod.addOre(4);
        return resourceMod;
    }
};

function ScienceLab(){

    this.name = function(){
        return "Science Lab";
    };

    this.produce = function(){
        var resourceMod = new playerResources();
        resourceMod.addTech(4);
        return resourceMod;
    };

    this.constructionCost = function(){
        var resourceMod = new playerResources();
        resourceMod.addOre(4);
        return resourceMod;
    }
};

function Spaceport(){

    this.name = function(){
        return "Spaceport";
    };
    this.produce = function(){
        return new playerResources();
    };

    this.constructionCost = function(){
        var resourceMod = new playerResources();
        resourceMod.addOre(4);
        return resourceMod;
    }
};