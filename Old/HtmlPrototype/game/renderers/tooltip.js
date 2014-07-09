function toolTipRenderer() {
    this.render = function(hoveredItem, system){

        var content = "<div id='hoverSummary' class='system-tip'>"
        content += system.name();
        content += "</div>";
        $(content).insertAfter(hoveredItem);

        $('#hoverSummary').css('left',window.mouseXPos).css('top',window.mouseYPos);
        $('#hoverSummary').delay(1000);
        $('#hoverSummary').fadeIn();
    };

    this.remove = function(hovererdItem){
        hovererdItem.css('background-color', 'black');
        $('#hoverSummary').remove();
    };
}