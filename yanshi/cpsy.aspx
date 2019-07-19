<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cpsy.aspx.cs" Inherits="xifeng_cpsy" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>产品溯源</title>
    <link href="css/cpsy.css" rel="stylesheet" />
 
    <link href="css/TJpublic.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         
        <div class="SY_infobox">
             
            <div class="SY_infotile" id="cptile">
                <img src="images/sy/jian.png" />
                <span>产品信息（示例）</span>
            </div>
            <div class="SY_ptinfobox SY_allinfobox ">
                <ul>
                    <li>产品名称：  <span runat="server">天鉴真酒 </span></li>
                    <li>配&nbsp&nbsp&nbsp&nbsp&nbsp  料：  <span runat="server">大豆、小麦、高粱 </span></li>
                    <li>批  次  号：  <span runat="server">20170718A032802 </span></li>
                    <li>发货时间：  <span runat="server">2018-07-15 </span></li>
                    <li>经  销  商：  <span runat="server">海南天元商店 </span></li>
                </ul>
            </div>
        </div>
        <div class="SY_infobox">
            <div class="SY_infotile" id="sytitle">
                <img src="images/sy/add.png" />
                <span>溯源信息</span>
            </div>
            <div class="SY_gyinfobox  SY_allinfobox SY_load  ">
                <ul id="sy_allinfo">
                    <li class="SY_gyfirstinfo">
                        <div class="SY_syinfotile">
                            <img src="images/sy/gyjian.png"><span>原料</span></div>
                        <div class="SY_syinfocontent" >
                            <div class="SY_syinfoylbox"><span>大豆</span><p>产地：黑龙江省</p>
                                <p>供应商：黑龙江丽水集团</p>
                            </div>
                            <div class="SY_syinfoylbox"><span>豌豆</span><p>产地：陕西省</p>
                                <p>供应商：天香集团</p>
                            </div>
                            <div class="SY_syinfoylbox"><span>小麦</span><p>产地：陕西省</p>
                                <p>供应商：海天有限责任公司</p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="SY_syinfotile">
                            <img src="images/sy/gyadd.png"><span>制曲</span></div>
                        <div class="SY_syinfocontent SY_load" >
                            <div class="SY_syinfoylbox"><span>传统大曲</span><p>时间：2016年7月12日</p>
                            </div>
                            <div class="SY_syinfoylbox"><span>新型大曲</span><p>时间：2016年7月12日</p>
                            </div>
                            <div class="SY_syinfoylbox"><span>小麦曲</span><p>时间：2016年7月12日</p>
                                <p>质检员：张三</p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="SY_syinfotile">
                            <img src="images/sy/gyadd.png"><span>制酒</span></div>
                        <div class="SY_syinfocontent SY_load">
                            <p>名称：***型</p>
                            <p>时间：2017-07-06</p>
                            <p>检验员：张三</p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_syinfotile">
                            <img src="images/sy/gyadd.png"><span>灌装</span></div>
                        <div class="SY_syinfocontent SY_load">
                            <p>车间：806车间</p>
                            <p>灌装小组：01</p>
                            <p>质检员：张三</p>
                            <p>时间：2017-07-06</p>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="SY_infobox">
            <div class="SY_infotile" id="gytitle1">
                <img src="images/sy/add.png" />
                <span>工艺流程</span>
            </div>
            <div class="SY_gyinfobox  SY_allinfobox SY_load  ">
                <ul>
                    <li class="SY_gyfirstinfo">
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyjian.png" />
                            <span>原料种植</span>
                        </div>
                        <div class="SY_gyinfocontent">
                            <p>
                                <img src="images/sy/gy_ylzz.png" />
                                小麦、大麦、豌豆以山泉水灌溉并使用有机肥，天然无公害。
                            </p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>原料粉粹</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_ylfs.png" />
                                采用最新粉碎技术，粉碎精细度适中使颗粒淀粉暴露出来，增加原料表面积，
                                有利于淀粉颗粒的吸水膨胀和蒸煮糊化，糖化时增加与酶的接触，为糖化发酵创造良好的条件。
                            </p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>配    料</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_pl.png" />
                                做到“稳、准、细、净”，对原料用量、配醅加糠的数量比例等要严格控制，并根据原料性质、气候条件进行必要调节，
                                发酵的稳定。多种原料混合使用，充分利用了各种粮食资源，而且能给微生物提供全面的营养成分，
                                原料中的有用成分经过微生物发酵代谢，产生多种副产物，使酒的香味、口味更为协调丰满。
                            </p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>蒸煮糊化</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_zzfh.png" />
                                利用蒸煮使淀粉糊化。蒸煮的温度和时间视原料种类、破碎程度等而定。
                                蒸煮后外观蒸透,熟而不粘,内无生心。
                            </p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>冷    却</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_lq.png" />
                                用扬渣或晾渣的方法,使料迅速冷却,使之达到微生物适宜生长的温度。
                            </p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>拌培发酵</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_bpfj.png" />
                                采用边糖化边发酵的双边发酵工艺,扬渣之后,同时加入曲子和酒母。
                                酒曲的用量适当，入窖的醅料适当装好后,在醅料上盖上一层糠,用窖泥密封,再加上一层糠。
                            </p>
                        </div>
                    </li>
                    <%--    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>蒸    酒</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_zj.png" />
                                通过蒸酒把醅中的酒精、水、高级醇、酸类等有效成分蒸发为蒸汽,再经冷却。
                            </p>
                        </div>
                    </li>--%>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>酒海贮存</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_jh.jpg" />
                                西凤酒海，是直径两米至两米五左右、高约三米的柱体容器，其原料选择、制作工艺于当今技术来讲仍繁复异常。
                            </p>
                        </div>
                    </li>
                    <li>
                        <div class="SY_gyinfotile">
                            <img src="images/sy/gyadd.png" />
                            <span>灌    装</span>
                        </div>
                        <div class="SY_gyinfocontent SY_load">
                            <p>
                                <img src="images/sy/gy_gz.png" />
                                采用先进流水线灌装，灌装流程细化灌装后的酒体达到清澈、透明、无悬浮物、无沉淀。
                            </p>
                        </div>
                    </li>

                </ul>
            </div>
        </div>

        <input type="hidden" runat="server" id="TcompID" value="2" />
        <input type="hidden" runat="server" id="TcpID" />
    </form>
  
    <script src="js/jquery-2.1.1.min.js"></script>
    <script src="js/TJpublic.js"></script>
    <script src="js/cpsynew.js"></script>
  
</body>
</html>
