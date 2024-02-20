/* globals Chart:false, feather:false */

/* globals Chart:false, feather:false */
function translatesite(site) {
    var site_name;
	switch (site) {
	  case 1:
		site_name = "荔湾西村";
		break;
	  case 2:
		site_name = "海珠宝岗";
		break;
	  case 3:
		site_name = "公园前";
		break;
	  case 4:
		site_name = "天河体育西";
		break;
	  case 5:
		site_name = "越秀麓湖";
		break;
	  case 6:
		site_name = "海珠赤沙";
		break;
	  case 7:
		site_name = "黄埔大沙地";
		break;
	  case 8:
		site_name = "番禺市桥";
		break;
	  case 9:
		site_name = "花都新华";
		break;
	  case 10:
		site_name = "黄埔镇龙";
		break;
	  case 11:
		site_name = "帽峰山";
		break;
	  case 12:
		site_name = "从化街口";
		break;
	  case 13:
		site_name = "增城荔城";
		break;
	  case 14:
		site_name = "增城新塘";
		break;
	  case 15:
		site_name = "黄埔永和";
		break;
	  case 16:
		site_name = "黄埔科学城";
		break;
	  case 17:
		site_name = "黄埔西区";
		break;
	  case 18:
		site_name = "黄埔文冲";
		break;
	  case 19:
		site_name = "白云嘉禾";
		break;
	  case 20:
		site_name = "天河奥体";
		break;
	  case 21:
		site_name = "天河龙洞";
		break;
	  case 22:
		site_name = "海珠沙园";
		break;
	  case 23:
		site_name = "番禺大石";
		break;
	  case 24:
		site_name = "番禺沙湾";
		break;
	  case 25:
		site_name = "南沙黄阁";
		break;
	  case 26:
		site_name = "南沙蒲州";
		break;
	  case 27:
		site_name = "南沙新垦";
		break;
	  case 28:
		site_name = "杨箕路边站";
		break;
	  case 29:
		site_name = "黄沙路边站";
		break;
	  case 30:
		site_name = "荔湾芳村";
		break;
	  case 31:
		site_name = "白云竹料";
		break;
	  case 32:
		site_name = "海珠沙园";
		break;
	  case 33:
		site_name = "白云山";
		break;
	  case 34:
		site_name = "番禺亚运城";
		break;
	  case 35:
		site_name = "番禺大夫山";
		break;
	  case 36:
		site_name = "增城派潭";
		break;
	  case 37:
		site_name = "从化良口";
		break;
	  case 38:
		site_name = "花都梯面";
		break;
	  case 39:
		site_name = "花都花东";
		break;
	  case 40:
		site_name = "花都赤坭";
		break;
	  case 41:
		site_name = "增城中新";
		break;
	  case 42:
		site_name = "增城石滩";
		break;
	  case 43:
		site_name = "白云江高";
		break;
	  case 44:
		site_name = "白云石井";
		break;
	  case 45:
		site_name = "白云新市";
		break;
	  case 46:
		site_name = "凤凰山";
		break;
	  case 47:
		site_name = "天河五山";
		break;
	  case 48:
		site_name = "番禺南村";
		break;
	  case 49:
		site_name = "南沙榄核";
		break;
	  case 50:
		site_name = "南沙沙螺湾";
		break;
	  case 51:
		site_name = "南沙街";
		break;
	  case 52:
		site_name = "番禺大学城";
		break;
	  case 1001:
		site_name = "越秀区";
		break;
	  case 1002:
		site_name = "海珠区";
		break;
	  case 1003:
		site_name = "荔湾区";
		break;
	  case 1004:
		site_name = "天河区";
		break;
	  case 1005:
		site_name = "白云区";
		break;
	  case 1006:
		site_name = "黄埔区";
		break;
	  case 1007:
		site_name = "花都区";
		break;
	  case 1008:
		site_name = "番禺区";
		break;
	  case 1009:
		site_name = "南沙区";
		break;
	  case 1010:
		site_name = "萝岗区";
		break;
	  case 1011:
		site_name = "从化区";
		break;
	  case 1012:
		site_name = "增城区";
		break;
	  case 1013:
		site_name = "路边站";
		break;
	  case 1014:
		site_name = "对照点";
		break;
	  case 4401:
		site_name = "广州市";
		break;
	  case 4402:
		site_name = "广州市20";
		break;
	  default:
		site_name = site;
	}
    return site_name
}


function generateArray (start, end) {
      return Array.from(new Array(end + 1).keys()).slice(start)
    }

