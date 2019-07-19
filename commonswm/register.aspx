<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="full-screen" content="yes" />
    <meta name="x5-fullscreen" content="true" />
    <meta name="description" content="三维码激活" />
    <meta content="telephone=no" name="format-detection" />
    <title>注册信息</title>
    <script>
        const ohtml = document.documentElement;
        function getsize() {
            var size = ohtml.clientWidth;
            if (size > 640) size = 640;
            ohtml.style.fontSize = size / 20 + 'px';
        }
        getsize();
        window.onresize = function () {
            getsize();
        }
    </script>
    <script src="../js/vue.min.js"></script> 
    
    <style>
        * {
            margin: 0;
            padding: 0;
        }

        .header {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            height: 2.5rem;
            line-height: 2.5rem;
            font-size: .9rem;
            text-align: center;
            color: #FFF;
            background-color: #26a2ff;
        }

        .item-container {
            background-color: #FFF;
        }

        .item {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 1rem;
            border-bottom: 1px solid #f5f5f5;
        }

        .item-title {
            font-size: .85rem;
            width: 4rem;
        }

        .item-input {
            width: 14rem;
            font-size: .8rem;
            border: none;
            margin-left: .5rem;
            padding: 1rem .2rem;
            color: gray;
        }

        .item-textarea {
            flex-grow: 1;
            padding: .3rem .2rem;
            margin-left: .5rem;
            height: 4rem;
            resize: none;
            border: none;
            font-size: .8rem;
            color: gray;
        }

        .btn {
            width: 90%;
            margin: 1rem auto;
            background-color: #26a2ff;
            color: #FFF;
            padding: .5rem 0;
            border-radius: 5px;
            text-align: center;
            font-size: .85rem;
        }

        .map-container {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 12;
        }

        .item-photo-edit {
            width: 5rem;
            height: 5rem;
            border: 2px dashed lightgray;
            border-radius: 3px;
            text-align: center;
            font-size: .7rem;
            color: gray;
        }

            .item-photo-edit img {
                width: 2rem;
                margin-top: 1.2rem;
            }

        .item-photo-container {
            padding: .5rem 0;
            position: relative;
            width: 13.5rem;
            margin-left: 0.5rem;
        }

        .item-photo-flie {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1;
            opacity: 0
        }

        .popup-choose {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 20;
            background-color: rgba(0,0,0,.5);
        }

        .choose-container {
            width: 80%;
            margin: 40% auto 0;
            background-color: #FFF;
            border-radius: 5px;
        }

        .choose-wrapper {
            padding: 1rem;
        }

        .choose-subtitle {
            text-align: center;
            font-size: .9rem;
            padding-bottom: 1rem;
            border-bottom: 1px solid #f5f5f5;
        }

        .choose-rdo-container {
            display: flex;
            justify-content: center;
            margin-top: 1rem;
        }

        .choose-rdo-wrapper {
            font-size: .85rem;
            margin-bottom: .5rem;
            width: 50%;
            text-align: center
        }

        .item-select {
            font-size: .8rem;
            color: gray;
            flex-grow: 1;
            margin-left: .5rem;
            padding: .3rem 0;
            border: none
        }

        .item-photo-it {
            width: 70%
        }
    </style>
</head>
<body style="background-color: #F5f5f5;">
    <form id="form1" runat="server">
        <div id="vim">
                <div class="header">
                    注册您的信息
                </div> 
            <div class="popup-choose" v-if="show.popup">
                <div class="choose-container">
                    <div class="choose-wrapper"> 
                         <p class="choose-subtitle">请指定三维码使用对象类型</p>
                        <div class="choose-rdo-container"> 
                        <div class="choose-rdo-wrapper">
                            <input type="radio" name="type" value="company" v-model="type"  />
                            <span>公司或单位</span>
                        </div>
                        <div class="choose-rdo-wrapper">
                            <input type="radio" name="type" value="person" v-model="type" />
                            <span>个人</span>
                        </div>
                        </div>
                        <div class="btn" style="width:40%;margin-bottom:1rem;" @click="closepopup">确定</div>
                    </div>
                </div>
            </div>

            <div  style="margin-top: 2.5rem"> 
               <div v-if="type == 'company' ">
                <div class="item-container">
                    <div class="item">
                        <span class="item-title">单位名称</span>
                        <input class="item-input" type="text" value="" placeholder="请输入单位名称" v-model="company.name" />
                    </div>
                    <div class="item" style="padding: 1rem;">
                        <span class="item-title">公司类别</span>
                        <select v-model="company.type_choose" class="item-select">
                           <option v-for="t in company.type" v-bind:value="t.id">{{t.text}}</option>
                        </select>
                    </div>
                    <div class="item">
                        <span class="item-title">企业简介</span>
                        <textarea class="item-textarea" placeholder="请输入您企业的信息" v-model="company.intro"></textarea>
                    </div>
                    <div class="item">
                        <span class="item-title">负责人</span>
                        <input class="item-input" type="text" value="" placeholder="请输入负责人姓名"  v-model="company.master" />
                    </div>
                    <div class="item">
                        <span class="item-title">手机</span>
                        <input class="item-input" type="number" value="" placeholder="请输入手机号码" v-model="company.phone" />
                    </div>
                    <div class="item">
                        <span class="item-title">地址</span>
                        <input class="item-input" type="text" value="" placeholder="点击我获取地址" readonly="readonly" v-model="address.poiaddress" @click="showmap" />
                    </div>
                    <div class="item"> 
                        <span class="item-title">营业执照</span>
                        <div class="item-photo-container">
                            <img class="item-photo-it" v-if="company.lisenseimg!=null" :src="company.lisenseimg" />
                            <div v-else class="item-photo-edit">
                                <img src="img/icon_camrea.png" />
                                <p>点击上传</p> 
                            </div>
                            <input type="file" class="item-photo-flie" @change="uplisenseimg" />
                        </div>
                    </div>
                </div>
                <div class="btn" @click="upit_company">提交信息</div>
            </div>
               <div v-else>
                <div class="item-container"> 
                     <div class="item">
                        <span class="item-title">负责人</span>
                        <input class="item-input" type="text" value="" placeholder="请输入负责人姓名" v-model="person.name" />
                    </div>
                    <div class="item">
                        <span class="item-title">手机</span>
                        <input class="item-input" type="number" value="" placeholder="请输入手机号码" v-model="person.phone"  />
                    </div>
                     <div class="item">
                        <span class="item-title">地址</span>
                        <input class="item-input" type="text" value="" placeholder="点击我获取地址" readonly="readonly" v-model="address.poiaddress" @click="showmap" />
                    </div>
                    <div class="item">
                        <span class="item-title">身份证</span>
                        <input class="item-input" type="text" value="" placeholder="请输入身份证号码" v-model="person.code"  />
                    </div>
                    <div class="item">
                         <span class="item-title">身份证正面</span>
                        <div class="item-photo-container">
                          <img class="item-photo-it" v-if="person.codeimg!=null" :src="person.codeimg" />
                            <div v-else class="item-photo-edit">
                                <img src="img/icon_camrea.png" />
                                <p>点击上传</p> 
                            </div>
                            <input type="file" class="item-photo-flie" @change="upcodeimg" />
                        </div>
                    </div>
                <div class="btn" @click="upit_person">提交信息</div>
                </div>
            </div>
            </div> 

            <div class="map-container" v-show="show.map">
                <iframe width="100%" height="100%" src="http://apis.map.qq.com/tools/locpicker?search=1&type=1&key=MWKBZ-DP7A6-IDOSB-MDDUB-JD66S-7QFYW&referer=myapp" frameborder="0"></iframe>
            </div>
        </div>
        <input id="inputopenid" type="hidden" runat="server"/>
          <input id="inputpid" type="hidden" runat="server"/>
    </form>
    <script src="../include/js/axios.min.js"></script>
    <script src="js/dealImage.js"></script>
    <script type="text/javascript" src="http://res.wx.qq.com/open/js/jweixin-1.4.0.js"></script>
    <script> 
        const vim = new Vue({
            el: "#vim",
            data: {
                type: 'company',
                show: {
                    map: false,
                    popup: true
                },
                address: {
                    poiaddress: null,
                    poiname: null,
                    cityname: null,
                    latlng: {
                        lat: 0,
                        lng: 0
                    }
                },
                company: {
                    name: null,
                    type: [
                        { id: 47, text: '生产厂家' }, { id: 48, text: '经销商' }
                    ],
                    type_choose: 47,
                    intro: null,
                    master: null,
                    phone: null,
                    lisenseimg: null
                },
                person: {
                    name: null,
                    phone: null,
                    code: null,
                    codeimg: null
                }
            },
            methods: {
                showmap: function () {
                    this.show.map = true;
                },
                closepopup: function () {
                    this.show.popup = false
                },
                upit_company: function () {
                    const c_name = this.company.name;
                    const c_type = this.company.type_choose;
                    const c_intro = this.company.intro;
                    const c_master = this.company.master;
                    const c_phone = this.company.phone;
                    const c_lisenseimg = this.company.lisenseimg;
                    var vm = this;
                    if (c_name != null && c_type != null && c_intro != null && c_master != null && c_phone != null) {
                        if (this.checkaddress()) {

                            const poiaddress = this.address.poiaddress;
                            const poiname = this.address.poiname;
                            const cityname = this.address.cityname;
                            const lat = this.address.latlng.lat;
                            const lng = this.address.latlng.lng;
                            let formdata = new FormData();
                            formdata.append("c_name", c_name);
                            formdata.append("c_type", c_type);
                            formdata.append("c_intro", c_intro);
                            formdata.append("c_master", c_master);
                            formdata.append("c_phone", c_phone);
                            if (c_lisenseimg != null)
                                formdata.append("c_lisenseimg", dataURLtoBlob(c_lisenseimg));
                            formdata.append("c_addr", poiaddress);
                            formdata.append("c_addrname", poiname);
                            formdata.append("c_city", cityname);
                            formdata.append("lat", lat);
                            formdata.append("lng", lng);
                            formdata.append("openid", document.getElementById("inputopenid").value);

                            axios({
                                method: 'post',
                                url: 'ajax/companyregister.ashx',
                                processData: false,
                                contentType: false,
                                data: formdata
                            }).then(function (msg) {
                                if (msg.data.rs == 100) {
                                    alert(msg.data.nr);
                                    vm.closewindow();
                                } else {
                                    alert(msg.data.nr);
                                }
                            });
                        }
                        else
                            alert("请选择地理信息！");
                    }
                    else
                        alert("请输入完整的信息!");
                },
                upit_person: function () {
                    const p_name = this.person.name;
                    const p_phone = this.person.phone;
                    const p_code = this.person.code;
                    const p_codeimg = this.person.codeimg;
                    var vm = this;
                    if (p_name != null && p_phone != null && p_code != null) {
                        if (this.checkaddress()) {

                            const poiaddress = this.address.poiaddress;
                            const poiname = this.address.poiname;
                            const cityname = this.address.cityname;
                            const lat = this.address.latlng.lat;
                            const lng = this.address.latlng.lng;

                            let formdata = new FormData();
                            formdata.append("p_name", p_name);
                            formdata.append("p_phone", p_phone);
                            formdata.append("p_code", p_code);
                            if(p_codeimg!=null)
                             formdata.append("p_codeimg", dataURLtoBlob(p_codeimg));
                            formdata.append("p_addr", poiaddress);
                            formdata.append("p_addrname", poiname);
                            formdata.append("p_city", cityname);
                            formdata.append("lat", lat);
                            formdata.append("lng", lng);
                            formdata.append("openid", document.getElementById("inputopenid").value);
                            axios({
                                method: 'post',
                                url: 'ajax/companyregisterswmperson.ashx',
                                processData: false,
                                contentType: false,
                                data: formdata
                            }).then(function (msg) {
                                if (msg.data.rs == 100) {
                                    alert(msg.data.nr);

                                    vm.closewindow();
                                } else {
                                    alert(msg.data.nr);
                                }
                            });
                        }
                        else
                            alert("请选择地理信息！");
                    }
                    else
                        alert("请输入完整的信息!");
                },
                checkaddress: function () {
                    if (this.address.cityname != null && this.address.poiaddress != null)
                        return true;
                    return false;
                },
                uplisenseimg: function (e) {
                    const file = e.target.files[0];
                    dealImage({ file: file, quality: 0.7 }, function (base) {
                        vim.company.lisenseimg = base;
                    })
                },
                upcodeimg: function (e) {
                    const file = e.target.files[0];
                    dealImage({ file: file, quality: 0.7 }, function (base) {
                        vim.person.codeimg = base;
                    })
                },
                closewindow: function () {
                    window.location.href = "swmactive.aspx?openid=" + document.getElementById("inputopenid").value + "&pid=" + document.getElementById("inputpid").value;
                    //WeixinJSBridge.call('closeWindow');
                }
            },
            created: function () {
                document.getElementById("vim").style.display = "block";

            },
            mounted: function () {
            }
        })

        window.addEventListener("message", function (event) {
            const loc = event.data;
            if (loc && loc.module == "locationPicker") {
                vim.address = loc;
                vim.show.map = false;
            }
        }, false);
    </script>  
</body>
</html>
