
function playerAreaRender(){

    this.render = function(context, player){


        $(".player-number").text(player.playerNumber());
        $("#activePlayerName").text(player.playerName());
        $("#player-ore-income").text(player.resources().ore());
        $("#player-food-income").text(player.resources().food());
        $("#player-tech-income").text(player.resources().tech());
        $('#player-actionqueue').html('');

        $.each(context.thisTurn().actionQueue(), function(key, value) {
            var action = "<div>"+value.summary()+"</div>";
            $('#player-actionqueue').append(action);
        });
    };

}