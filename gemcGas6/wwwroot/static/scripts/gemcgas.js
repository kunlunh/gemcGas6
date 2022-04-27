$(function(){
		var _nava= $('.nav .nav-item a');
		var _url = window.location.href;
		var _host = window.location.host;
		for(var i = 0; i<_nava.length ; i++){
			var _astr = _nava.eq(i).attr('href');
            if(_url.indexOf(_astr) != -1){
               _nava.eq(i).addClass('active').siblings().removeClass('active');
            }else if(_url == ('http://'+_host+'/')){
            	_nava.eq(0).addClass('active').siblings().removeClass('active');
            }
		}
})