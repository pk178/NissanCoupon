var app = angular.module("nissanCoupon", ['pascalprecht.translate', 'ngCookies']);

app.config(["$translateProvider", function ($translateProvider) {

    angular.lowercase = angular.$$lowercase;

    var en_translations = {
        "KiemTraCoupon": "Check coupon",
        "TaoMoiCoupon": "Upload coupon",
        "CouponDaSuDung": "Redeemed list",
        "DanhSachCoupon": "Coupon list",
        "QuanLyNguoiDung": "User manager",
        "DangNhapHeThong": "Login to system",
        "TenDangNhap": "Username",
        "MatKhau": "Password",
        "NhapLaiMatKhau": "Reenter password",
        "Quyen": "Permission",
        "DangNhap": "Login",
        "DangXuat": "Logout",
        "TuNgay": "From date",
        "DenNgay": "To date",
        "TimKiem": "Search",
        "Them": "New user",
        "Sua": "Edit",
        "TenDayDu": "Full name",
        "SoDienThoai": "Phone number",
        "DaiLy": "Dealer",
        "ThongTinNguoiDung": "User info",
        "Thoat": "Close",
        "CapNhat": "Update",
        "ChonFile": "Choose file to upload",
        "TaiFileMau": "Download template",
        "Xuat": "Export",
        "MaCoupon": "Coupon",
        "KiemTra": "Check",
        "BanCoMuonSuDungCouponKhong": "Bạn có muốn sử dụng coupon không",
        "Co": "Yes",
        "Khong": "No",
        "NhapOtp": "Enter OTP",
        "MaOtp": "OTP",
        "XacNhan": "Confirm",
        "Category": "Category",
        "Type": "Type",
        "PromotionDate": "Promotion date",
        "DealerName": "Dealer name",
        "CustomerNumber": "Customer number",
        "CustomerName": "Customer name",
        "PhoneNumber": "Phone number",
        "VehicleModel": "Vehicle model",
        "ChassisNumber": "Chassis number",
        "LicensePlateNumber": "License plate number",
        "GiftCode": "Gift code",
        "ExpriedDate": "Expried date",
        "ReminderDay": "Reminder day",
        "EntitledServiceGift": "Entitled service gift",
        "RedeemedDate": "Redeemed date",
        "RedeemedByDealer": "Redeemed by dealer",
        "CampaignName": "Campaign name",
        "TenDangNhapHoacMatKhauKhongDung": "Username or password is not correct."
    };

    var vi_translations = {
        "KiemTraCoupon": "Kiểm tra coupon",
        "TaoMoiCoupon": "Tạo mới coupon",
        "CouponDaSuDung": "Coupon đã sử dụng",
        "DanhSachCoupon": "Danh sách coupon",
        "QuanLyNguoiDung": "Quản lý người dùng",
        "DangNhapHeThong": "Đăng nhập hệ thống",
        "TenDangNhap": "Tên đăng nhập",
        "MatKhau": "Mật khẩu",
        "NhapLaiMatKhau": "Nhập lại mật khẩu",
        "Quyen": "Quyền",
        "DangNhap": "Đăng nhập",
        "DangXuat": "Đăng xuất",
        "TuNgay": "Từ ngày",
        "DenNgay": "Đến ngày",
        "TimKiem": "Tìm kiếm",
        "Them": "Thêm mới",
        "Sua": "Sửa",
        "TenDayDu": "Tên đầy đủ",
        "SoDienThoai": "Số điện thoại",
        "DaiLy": "Đại lý",
        "ThongTinNguoiDung": "Thông tin người dùng",
        "Thoat": "Thoát",
        "CapNhat": "Cập nhật",
        "ChonFile": "Chọn file để tải lên",
        "TaiFileMau": "Tải file mẫu",
        "Xuat": "Xuất excel",
        "MaCoupon": "Mã coupon",
        "KiemTra": "Kiểm tra",
        "BanCoMuonSuDungCouponKhong": "Bạn có muốn sử dụng coupon không",
        "Co": "Có",
        "Khong": "Không",
        "NhapOtp": "Nhập mã OTP",
        "MaOtp": "Mã OTP",
        "XacNhan": "Xác nhận",
        "Category": "Nhóm",
        "Type": "Loại chiến dịch",
        "PromotionDate": "Ngày khảo sát",
        "DealerName": "Tên đại lý",
        "CustomerNumber": "Mã số KH",
        "CustomerName": "Tên KH",
        "PhoneNumber": "Số điện thoại",
        "VehicleModel": "Loại xe",
        "ChassisNumber": "Số khung",
        "LicensePlateNumber": "Biển số xe",
        "GiftCode": "Mã quà tặng",
        "ExpriedDate": "Ngày hết hạn",
        "ReminderDay": "Nhắc trước ngày",
        "EntitledServiceGift": "Quà tặng",
        "RedeemedDate": "Ngày sử dụng",
        "RedeemedByDealer": "Đại lý sử dụng",
        "CampaignName": "Tên chiến dịch",
        "TenDangNhapHoacMatKhauKhongDung": "Tên đăng nhập hoặc mật khẩu không đúng."
    };

    $translateProvider.translations('en', en_translations);

    $translateProvider.translations('vi', vi_translations);

    $translateProvider.preferredLanguage('vi');

}]);

app.directive('fileUpload', ["$rootScope", "$http", function ($rootScope, $http) {
    return {
        restrict: 'A',
        scope: true,
        link: function (scope, element, attrs) {
            element.bind('change', function () {
                var formData = new FormData();
                formData.append('file', element[0].files[0]);
                $http({
                    method: "post",
                    url: "/Coupon/UploadFile",
                    data: formData,
                    headers: { 'Content-Type': undefined }
                }).then(function (res) {
                    $rootScope.result = res.data;
                    element[0].value = "";
                });
            });
        }
    };
}]);

app.directive('datePicker', function () {
    return {
        scope: true,
        link: function (scope, element, attrs) {
            element.datepicker({
                dateFormat: 'dd/mm/yy'
            });
        }
    };
});

app.run(["$rootScope", "$cookies", "$translate", function ($rootScope, $cookies, $translate) {
    var lang = $cookies.get('lang');
    $translate.use(lang);
    $rootScope.changeLanguage = function (lang) {
        $translate.use(lang);
        $cookies.put('lang', lang, { path: "/" });
    };
}]);

app.controller("loginController", ["$scope", '$http', function ($scope, $http) {
    $scope.user = {
        UserName: '',
        Password: ''
    };
    $scope.login = function () {
        if ($scope.form.$valid) {
            $http({
                method: "post",
                url: "/Home/Login",
                data: $scope.user
            }).then(function (res) {
                if (res.data.success)
                    location.href = "/";
                else
                    $scope.message = res.data.message;
            });
        }
    };
}]);

app.controller("userController", ["$scope", '$http', function ($scope, $http) {
    $scope.loadUsers = function () {
        $http({
            method: "get",
            url: "/User/GetAll"
        }).then(function (res) {
            $scope.users = res.data;
        });
    };
    $scope.loadDealers = function () {
        $http({
            method: "get",
            url: "/User/GetDealers"
        }).then(function (res) {
            $scope.dealers = res.data;
        });
    };
    $scope.addUser = function () {
        $scope.user = {
            Dealer: { Code: "00" },
            Role: "0"
        };
        $('#myModal').modal('show');
    };
    $scope.editUser = function (index) {
        $scope.user = $scope.users[index];
        if ($scope.user.Permission && $scope.user.Permission.length > 0)
            $scope.user.Role = "1";
        else
            $scope.user.Role = "0";
        $('#myModal').modal('show');
    };
    $scope.updateUser = function () {
        $scope.user.Dealer = $scope.dealers.find(x => x.Code === $scope.user.Dealer.Code);
        if ($scope.user.Role === "1")
            $scope.user.Permission = [92, 182];
        else
            $scope.user.Permission = null;
        if ($scope.form.$valid) {
            if ($scope.user.RePassword && $scope.user.RePassword !== $scope.user.Password)
                $scope.message = "Nhập lại mật khẩu không đúng";
            else {
                $http({
                    method: "post",
                    url: "/User/Update",
                    data: $scope.user
                }).then(function (res) {
                    if (res.data) {
                        $scope.loadUsers();
                    }
                    $('#myModal').modal('hide');
                });
            }
        }
    };
    $scope.loadUsers();
    $scope.loadDealers();
}]);

app.controller("homeController", ["$scope", '$http', function ($scope, $http) {
    $scope.valid = false;
    $scope.confirm = false;
    $scope.success = false;
    $scope.check = function () {
        if ($scope.formCheck.$valid) {
            $http({
                method: "post",
                url: "/Coupon/CheckCoupon",
                params: { coupon: $scope.code }
            }).then(function (res) {
                $scope.coupon = res.data;
                if (!$scope.coupon.GiftCode) {
                    $scope.message = "Mã quà tặng/ khuyến mại không đúng. Vui lòng kiểm tra lại thông tin.";
                }
                else if ($scope.coupon.RedeemedByDealer) {
                    if ($scope.coupon.RedeemedByDealer == "Canceled") {
                        $scope.message = "Mã quà tặng/ khuyến mại này đã bị hủy do lỗi hệ thống, rất mong quý khách thông cảm và kiểm tra lại thông tin."
                    }
                    else {
                        $scope.message = "Mã quà tặng/ khuyến mại này đã được sử dụng vào ngày " + $scope.coupon.RedeemedDate + " tại Đại lý " + $scope.coupon.RedeemedByDealer + ". Vui lòng kiểm tra lại thông tin.";
                    }
                    
                }
                else {
                    var array = $scope.coupon.ExpriedDate.split('/');
                    var date = new Date(array[2], parseInt(array[1]) - 1, parseInt(array[0]) + 1);
                    var now = new Date();
                    if (date > now) {
                        $scope.message = null;
                        $scope.valid = true;
                    }
                    else {
                        $scope.message = "Mã quà tặng/ khuyến mại này đã hết hạn sử dụng vào ngày " + $scope.coupon.ExpriedDate + ". Vui lòng kiểm tra lại thông tin.";
                    }
                }
            });
        }
    };
    $scope.yes = function () {
        $http({
            method: "post",
            url: "/Coupon/CreateCouponOtp",
            params: { coupon: $scope.code }
        }).then(function (res) {
            if (res.data) {
                $scope.confirm = true;
            }
            else {
                $scope.message = "Có lỗi xảy ra, vui lòng thử lại sau.";
            }
        });
    };
    $scope.no = function () {
        location.reload();
    };
    $scope.use = function () {
        $http({
            method: "post",
            url: "/Coupon/CheckCouponOtp",
            params: { coupon: $scope.code, otp: $scope.otp }
        }).then(function (res) {
            if (res.data) {
                $scope.success = true;
                $scope.message = "Hệ thống đã ghi nhận lịch sử sử dụng mã quà tặng/ khuyến mại " + $scope.coupon.GiftCode + " tại Đại lý " + $scope.coupon.RedeemedByDealer + ". Đại lý vui lòng gửi hồ sơ thanh toán về NISSAN VIỆT NAM theo quy định đã được thông báo.";
            }
            else {
                $scope.message = "Mã xác nhận không đúng. Vui lòng kiểm tra lại thông tin.";
            }
        });
    };
}]);

app.controller("couponController", ["$scope", '$http', function ($scope, $http) {
    $scope.search = {
        from: "",
        to: ""
    };
    $scope.loadCoupons = function () {
        $http({
            method: "get",
            url: "/Coupon/GetAll",
            params: $scope.search
        }).then(function (res) {
            $scope.coupons = res.data;
        });
    };
    $scope.loadCoupons();
    $scope.export = function () {
        var link = document.createElement("a");
        link.href = 'data:application/vnd.ms-excel,' + encodeURIComponent($('table').parent().html());
        link.download = 'export.xls';
        link.click();
    };
}]);

app.controller("redeemedController", ["$scope", '$http', function ($scope, $http) {
    $scope.search = {
        from: "",
        to: ""
    };
    $scope.loadCoupons = function () {
        $http({
            method: "get",
            url: "/Coupon/GetRedeemed",
            params: $scope.search
        }).then(function (res) {
            $scope.coupons = res.data;
        });
    };
    $scope.loadCoupons();
    $scope.export = function () {
        var link = document.createElement("a");
        link.href = 'data:application/vnd.ms-excel,' + encodeURIComponent($('table').parent().html());
        link.download = 'export.xls';
        link.click();
    };
}]);

$(document).ready(function () {
    $('body').show();
});