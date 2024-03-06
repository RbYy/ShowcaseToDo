//import { Sortable, OnSpill } from '/Sortable.js';

//Sortable.mount(OnSpill);
window.getWidth = (element) => {
    console.log("dfgdsg");
    return element.offsetWidth;
}
window.getHeight = (element) => {
    console.log("dfgdsg22222");
    return element.offsetHeight;
}

window.addResizeListener = (dotnetHelper, methodName) => {
    window.addEventListener('resize', () => {
        dotnetHelper.invokeMethodAsync(methodName);
    });
}

window.getElementDimensions = (selector) => {
    var element = document.querySelector(selector);
    if (element) {
        return { 
            'height': element.offsetHeight, 
            'width': element.offsetWidth 
        };
    }
    return null;
}

//new Sortable(el, {
//	removeOnSpill: true, // Enable plugin
//	// Called when item is spilled
//	onSpill: function(/**Event*/evt) {
//		evt.item; // The spilled item
//        console.log("asdasdasdasdsad")
//	}
//});
