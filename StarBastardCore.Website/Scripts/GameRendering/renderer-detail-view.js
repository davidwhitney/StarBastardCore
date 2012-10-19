function detailedViewRenderer() {

    this.render = function(context, system){
        var $context = context;

        if(context == null) {
            return;
        }
        if(system == null) {
            return;
        }

        $('#zoomed-system-name').text(system.Name);
        /*$('#zoomed-number-of-buildings-built').text(system.City.length);
        $('#zoomed-number-of-buildings-cap').text(system.BuildingCap);
        $('#zoomed-number-of-armies-in-battlefield').text(system.Battlefield.length);
        $('#zoomed-number-of-ships-in-orbit').text(system.PlayerShipsInOrbit.length);
        $('#zoomed-number-of-enemy-ships-in-orbit').text(system.EnemyShipsInOrbit.length);
        $('#zoomed-occupier-id').text(system.Owner);

        var predictedResources = system.ResourceIncreasePrediction;

        $("#zoomed-ore-income").text(predictedResources.Ore);
        $("#zoomed-food-income").text(predictedResources.Food);
        $("#zoomed-tech-income").text(predictedResources.Tech);*/

        $('#zoomed-orbit-ships').html('');

        $.each(system.Orbit, function (key, value) {
            var item = "<div>"+ value.name() +"</div>";
            $('#zoomed-orbit-ships').append(item);
        });

        $('#zoomed-city-items').html('');
        $.each(system.City, function(key, value) {
            var item = "<div>"+ value.name() +"</div>";
            $('#zoomed-city-items').append(item);
        });

        $('#zoomed-in').fadeIn();
    };
}


