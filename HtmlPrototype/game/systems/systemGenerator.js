function systemGenerator() {

    this.generateSystems = function(){
        var playerOneSystems = this.generateSystemsForOnePlayer(1);
        var playerTwoSystems = this.generateSystemsForOnePlayer(2);

        var systems = new Array();

        var offset = 0;
        $.each(playerOneSystems, function(key, value) {
            systems[offset] = value;
            offset++;
        });
        $.each(playerTwoSystems, function(key, value) {
            systems[offset] = value;
            offset++;
        });

        return systems;
    };

    this.generateSystemsForOnePlayer = function(playerNumber){
        var systems = new Array();
        var points = this.generateScoresForSetOfPlanets();

        for(var i = 0; i != 37; i++){
            var name = names[generateRandomNumber(16455)];
            var oneSystem = new system(name, playerNumber + "_" + (i+1), points[i], playerNumber);
            systems[i] = oneSystem;
        }

        systems[18].orbit().push(new constructionStarship(playerNumber));


        return systems;
    };

    this.generateScoresForSetOfPlanets = function(){
        var points = new Array();
        var totalPoints = 100;

        while(totalPoints > 0){ // lets make sure we get allocated
            //generate planet points
            for(var p = 0; p != 37; p++){
                var randomBetweenOneAndTen = Math.floor((Math.random()*10)+1);
                if(randomBetweenOneAndTen > totalPoints){
                    randomBetweenOneAndTen = totalPoints;
                }
                totalPoints = totalPoints - randomBetweenOneAndTen;
                points[p] = randomBetweenOneAndTen;
            }
        }

        this.fisherYatesSort(points);

        // ensure the start position is workable.

        if(points[18] == 0){
            $.each(points, function(key, value) {
                if(value > 5){
                    points[18] = value;
                    points[key] = 0;
                }
            });
        }

        return points;
    };


    this.fisherYatesSort =  function ( myArray ) {
        var i = myArray.length;
        if ( i == 0 ) return false;
        while ( --i ) {
            var j = Math.floor( Math.random() * ( i + 1 ) );
            var tempi = myArray[i];
            var tempj = myArray[j];
            myArray[i] = tempj;
            myArray[j] = tempi;
        }
    };
}
   