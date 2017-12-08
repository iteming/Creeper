/**
 * Created by Administrator on 2017/1/5.
 */

// 左侧公共菜单
var app = angular.module('sider', []);
app.controller('siderCtrl', function ($scope, $sce) {
    $scope.notice = {content:''};
    $scope.newslist = [];
    $scope.categorylist = [];
    $scope.contact = {};

    $scope.loadNotice = function () {
        $.ajax({
            url: "/sider_notice",
            type: "get",
            dataType: "json",
            success: function (data) {
                if (data.Ret == 1) {
                    $scope.notice = data.Data;
                    $scope.notice.content = $sce.trustAsHtml(data.Data.content);
                    $scope.$apply();
                } else {
                    toastrShow(data.Ret, data.Message);
                }
            }
        });
    };
    $scope.loadNews = function () {
        $.ajax({
            url: "/sider_news",
            type: "get",
            dataType: "json",
            success: function (data) {
                if (data.Ret == 1) {
                    $scope.newslist = data.Data;
                    $scope.$apply();
                    tooltip();
                } else {
                    toastrShow(data.Ret, data.Message);
                }
            }
        });
    };
    $scope.loadCategory = function () {
        $.ajax({
            url: "/sider_category",
            type: "get",
            dataType: "json",
            success: function (data) {
                if (data.Ret == 1) {
                    $scope.categorylist = data.Data;
                    $scope.$apply();
                } else {
                    toastrShow(data.Ret, data.Message);
                }
            }
        });
    };
    $scope.loadContact = function () {
        $.ajax({
            url: "/sider_contact",
            type: "get",
            dataType: "json",
            success: function (data) {
                if (data.Ret == 1) {
                    $scope.contact = data.Data;
                    $scope.$apply();
                    $scope.loadindexmap(data.Data.location_lng, data.Data.location_lat);
                } else {
                    toastrShow(data.Ret, data.Message);
                }
            }
        });
    };
    $scope.loadindexmap = function (location_lng, location_lat) {
        // 百度地图API功能
        var map = new BMap.Map("indexmap");                         // 创建Map实例
        var point = new BMap.Point(location_lng, location_lat);     // 创建Point点
        map.centerAndZoom(point, 11);                               // 初始化地图,设置中心点坐标、地图缩放级别
        map.addControl(new BMap.MapTypeControl());                  // 添加地图类型控件
        map.enableScrollWheelZoom(true);                            // 启用滚轮放大缩小，默认禁用
        map.enableContinuousZoom(true);                             // 启用地图惯性拖拽，默认禁用
        var marker = new BMap.Marker(point);                        // 创建标注
        map.addOverlay(marker);                                     // 将标注添加到地图中
        marker.setAnimation(BMAP_ANIMATION_BOUNCE);                 // 跳动的动画
    };

    $scope.loadNotice();
    $scope.loadNews();
    $scope.loadCategory();
    $scope.loadContact();
});