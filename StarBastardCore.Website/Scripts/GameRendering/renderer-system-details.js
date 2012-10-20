function systemDetailsRenderer() {

    this.render = function(context, system){
        var $context = context;

        if(context == null) {
            return;
        }
        if(system == null) {
            return;
        }
        
        if (system.Name === "Undiscovered") {
            $('#zoomed-in').fadeOut();
            return;
        }

        $('#zoomed-system-name').text(system.Name);
        $('#zoomed-number-of-buildings-built').text(system.City.length);
        $('#zoomed-number-of-buildings-cap').text(system.BuildingCap);
        $('#zoomed-number-of-armies-in-battlefield').text(system.Battlefield.length);
        $('#zoomed-number-of-ships-in-orbit').text(system.PlayerShipsInOrbit.length);
        $('#zoomed-number-of-enemy-ships-in-orbit').text(system.EnemyShipsInOrbit.length);
        $('#zoomed-occupier-id').text(system.Owner.Name);

        var predictedResources = system.ResourceIncreasePrediction;

        $("#zoomed-ore-income").text(predictedResources.Ore);
        $("#zoomed-food-income").text(predictedResources.Food);
        $("#zoomed-tech-income").text(predictedResources.Tech);

        $('#zoomed-orbit-ships').html('');

        $.each(system.Orbit, function (key, value) {

            $('<div/>', {
                title: value.Name,
                text: 's',
                class: value.Name + ' IMovableUnitPopout',
            }).appendTo('#zoomed-orbit-ships');
            
        });

        $('#zoomed-city-items').html('');
        $.each(system.City, function(key, value) {
            
            $('<div/>', {
                title: value.Name,
                text: "building",
                class: value.Name + ' poptip',
                style: 'width: 50px; height: 50px; background-color: green;',
            }).appendTo('#zoomed-city-items');
            
        });
        

        $(".IMovableUnitPopout").click(function () {
            $(this).popover({
                html: 'true',
                content: 'Move',
            });
        });

        $('#zoomed-in').fadeIn();
    };
}


