
function playerStatusRenderer(){

    this.render = function(context, player){


        $(".player-number").text(player.UserId);
        $("#activePlayerName").text(player.Name);
        $("#player-ore-income").text(player.Resources.Ore);
        $("#player-food-income").text(player.Resources.Food);
        $("#player-tech-income").text(player.Resources.Tech);
        $('#player-actionqueue').html('');

        /*$.each(context.thisTurn().actionQueue(), function(key, value) {
            var action = "<div>"+value.summary()+"</div>";
            $('#player-actionqueue').append(action);
        });*/
    };

}