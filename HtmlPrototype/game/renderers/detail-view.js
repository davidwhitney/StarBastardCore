function detailedViewRenderer() {

    this.render = function(context, system){
        var $context = context;

        if(context == null) {
            return;
        }
        if(system == null) {
            return;
        }

        $('#zoomed-system-name').text(system.name());
        $('#zoomed-number-of-buildings-built').text(system.city().length);
        $('#zoomed-number-of-buildings-cap').text(system.buildingCap());
        $('#zoomed-number-of-armies-in-battlefield').text(system.battlefield().length);
        $('#zoomed-number-of-ships-in-orbit').text(system.playerShipsInOrbit().length);
        $('#zoomed-number-of-enemy-ships-in-orbit').text(system.enemyShipsInOrbit().length);
        $('#zoomed-occupier-id').text(system.owner());

        var predictedResources = system.resourceIncreasePrediction();

        $("#zoomed-ore-income").text(predictedResources.ore());
        $("#zoomed-food-income").text(predictedResources.food());
        $("#zoomed-tech-income").text(predictedResources.tech());

        $('#zoomed-orbit-ships').html('');
        $.each(system.orbit(), function(key, value) {
            var item = "<div>"+ value.name() +"</div>";
            $('#zoomed-orbit-ships').append(item);
        });

        $('#zoomed-city-items').html('');
        $.each(system.city(), function(key, value) {
            var item = "<div>"+ value.name() +"</div>";
            $('#zoomed-city-items').append(item);
        });

        $('#zoomed-in').fadeIn();
    };
}


