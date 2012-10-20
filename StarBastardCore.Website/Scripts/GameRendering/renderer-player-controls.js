function playerControls(updateUiFunction){

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

        var toggledHoverProperty = 'background-color';
        var previousToggledHoverColor = 'black';
        
        $(".system").hover(
            function () {
                
                previousToggledHoverColor = $(this).css(toggledHoverProperty);
                $(this).css(toggledHoverProperty, 'red');
                $(this).tooltip('show');
            },
            function () {
                $(this).css(toggledHoverProperty, previousToggledHoverColor);
            }
        );
        
        $(".poptip").hover(
            function () {
                $(this).tooltip('show');
            },
            function () {
            }
        );
        

    };
    
    this.getSelectedSystem = function () {
        return $('body').data('activeSystem');
    };

    this.setSelectedSystem = function (system) {
        $('body').data('activeSystem', system);
    };
    
};
