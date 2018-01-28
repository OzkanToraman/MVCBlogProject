$(function(){

  "use strict";
  
  // Owl slider Initialize

  $('#owl-slider').owlCarousel({
    loop:true,
    items:1,
    nav:true,
    navText:["<span class='icon-angle-left'></span>","<span class='icon-angle-right'></span>"],
    animateOut: 'fadeOut'
  });

});