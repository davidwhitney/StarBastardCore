function uiComponents(updateUiFunction){

    var $updateUiFunction = updateUiFunction;
    var $this = this;
    
    this.bindToUi = function(){

        $("body").mousemove(function(e){
            window.mouseXPos = e.pageX;
            window.mouseYPos = e.pageY;
        });

        $(".system").unbind('click');
        $(".system").click(function(){
            var id = $(this).data('id');
            var thisSystem = window.gameboard[id];
            $this.setSelectedSystem(thisSystem);

            $updateUiFunction(window.context);
        });

        $(".switch-to-map").click(function(){
            $('#zoomed-in').fadeOut();
        });


        $(".system").hover(
            function () {
                $(this).css('background-color', 'red');
                $(this).tooltip('show');
            },
            function () {
                $(this).css('background-color', 'black');
            }
        );

        $(".start-turn").click(function(){
            $('#welcome-help').hide();

            window.context.startTurn();
        });

        $(".build-farm").click(function(){
            build(new Farm());
        });
        $(".build-sciencelab").click(function(){
            build(new ScienceLab());
        });
        $(".build-mine").click(function(){
            build(new Mine());
        });
        $(".build-spaceport").click(function(){
            build(new Spaceport());
        });

        function build(building){
            var selectedSystem = $this.getSelectedSystem();
            if(selectedSystem == null){
                return;
            }
            var action = new buildAction(building, selectedSystem, window.context.currentPlayer());
            window.context.queueAction(action);
        };
    };


    this.getSelectedSystem = function () {
        return $('body').data('activeSystem');
    };

    this.setSelectedSystem = function (system) {
        $('body').data('activeSystem', system);
    };
    
};
