create database db_G06WNC

use db_G06WNC



create table Category
(
	iCategoryID int not null identity(1,1) primary key,
	sTitle ntext null
)

create table Content 
(
	iContentID int not null identity(1,1) primary key,
	sTitle ntext null,
	dCreatedate datetime,
	sMainbody ntext null,
	sSource ntext null,
	iCategoryID int null
	constraint FK_CategoryContent foreign key (iCategoryID) references Category(iCategoryID)
)
alter table Content
add sImage ntext null 


create table FileCategory
(
	iFileCategoryID int not null identity(1,1) primary key,
	sTitle ntext null
)

create table ResourceFile
(
	iResourceFileID int not null identity(1,1) primary key,
	sFilename ntext null,
	dUploaddate datetime null,
	sDescription ntext null,
	iCategoryID int null,

	constraint FK_FileCategoryResourceFile foreign key (iCategoryID) references FileCategory(iFileCategoryID),
)

--drop table ResourceFile

create table Account
(
	iAccountID int not null identity(1,1) primary key,
	sName ntext null,
	sEmail ntext null,
	sPassword ntext null,
	sPhone ntext null,
	sAvatar ntext null,
	dBirthofdate datetime null,
	iRoleID int null,

	constraint FK_RolesAccount foreign key (iRoleID) references Roles(iRoleID)
)

create table Roles
(
	iRoleID int not null identity(1,1) primary key,
	sRolename ntext null,
)


create table Feedback
(
	iFeedbackID int not null identity(1,1) primary key,
	sContent ntext null,
	iAccountID int null,
	iFeedbackdate datetime null,

	constraint FK_FeedbackAccount foreign key (iAccountID) references Account(iAccountID)
)

alter table Feedback
add sResponse ntext null

insert into Category(sTitle)
values
(N'Tin tức - sự kiện'),
(N'Hoạt động của hội'),
(N'Chính sách - pháp luật'),
(N'Tài liệu sinh hoạt hội viên'),
(N'Trái tim tình thương'),
(N'Kiến thức và kỹ năng')

select * from Category

insert into Roles(sRolename)
values
(N'Quản trị viên'),
(N'Hội viên')

select * from Roles

insert into FileCategory(sTitle)
values
(N'Ấn phẩm, tài liệu'),
(N'Video'),
(N'Hình ảnh')

select * from FileCategory
select * from ResourceFile


insert into Content(sTitle,dCreatedate,sMainbody,sSource,iCategoryID)
values
(N'Bà Võ Thị Ánh Xuân giữ quyền Chủ tịch nước','2024-03-21',N'Phó Chủ tịch nước Võ Thị Ánh Xuân giữ quyền Chủ tịch nước CHXHCN Việt Nam cho đến khi Quốc hội bầu ra Chủ tịch nước mới. Ngày 21/3, Ủy ban Thường vụ Quốc hội đã ban hành Thông báo số 5/TB-UBTVQH15 về việc thực hiện quyền Chủ tịch nước Cộng hòa xã hội chủ nghĩa Việt Nam.

Theo đó, bà Võ Thị Ánh Xuân, Phó Chủ tịch nước giữ quyền Chủ tịch nước Cộng hòa xã hội chủ nghĩa Việt Nam từ ngày 21/3/2024 cho đến khi Quốc hội bầu ra Chủ tịch nước mới.

Bà Võ Thị Ánh Xuân sinh năm 1970, quê ở huyện Tịnh Biên, tỉnh An Giang; Ủy viên Trung ương Đảng dự khuyết khóa XI và là Ủy viên Trung ương Đảng khóa XII và XIII; đại biểu Quốc hội khóa XIV, XV.

Quyền Chủ tịch nước Võ Thị Ánh Xuân từng đảm nhiệm các chức vụ: Phó Chủ tịch UBND tỉnh An Giang, Phó Bí thư Tỉnh ủy An Giang, Bí thư Tỉnh ủy An Giang.

Tại kỳ họp cuối cùng của Quốc hội khoá XIV, bà Võ Thị Ánh Xuân được bầu giữ chức Phó Chủ tịch nước CHXHCN Việt Nam nhiệm kỳ 2016 - 2021.

Tại kỳ họp thứ nhất, Quốc hội khoá XV tiếp tục bầu bà Võ Thị Ánh Xuân giữ chức Phó Chủ tịch nước CHXHCN Việt Nam nhiệm kỳ 2021 - 2026.',N'dangcongsan.vn', 1),
(N'Luật Đất đai (sửa đổi): Vai trò, trách nhiệm của Mặt trận Tổ quốc Việt Nam và các tổ chức thành viên của Mặt trận trong quản lý và sử dụng đất đai','2024-02-29',N'Câu hỏi: Vai trò, trách nhiệm của Mặt trận Tổ quốc Việt Nam và các tổ chức thành viên của Mặt trận trong quản lý và sử dụng đất đai được quy định như thế nào trong Luật Đất đai (sửa đổi)? 
Trả lời: Theo Điều 19, Luật Đất đai (sửa đổi) được Quốc hội nước Cộng hòa xã hội chủ nghĩa Việt Nam khóa XV, kỳ họp bất thường lần thứ năm thông qua ngày 18/01/2024 quy định vai trò, trách nhiệm của Mặt trận Tổ quốc Việt Nam và các tổ chức thành viên của Mặt trận trong quản lý và sử dụng đất đai như sau:

1. Ủy ban Trung ương Mặt trận Tổ quốc Việt Nam tham gia xây dựng pháp luật, thực hiện phản biện xã hội đối với dự thảo văn bản pháp luật về đất đai, dự thảo quy hoạch, kế hoạch sử dụng đất quốc gia, dự án có sử dụng đất do Quốc hội, Thủ tướng Chính phủ quyết định, chấp thuận chủ trương đầu tư.

2. Ủy ban Mặt trận Tổ quốc Việt Nam các cấp có trách nhiệm sau đây:

a) Tham gia xây dựng pháp luật; thực hiện phản biện xã hội đối với dự thảo văn bản pháp luật về đất đai, dự thảo quy hoạch, kế hoạch sử dụng đất cùng cấp, dự án có sử dụng đất do Hội đồng nhân dân, Ủy ban nhân dân cùng cấp quyết định, chấp thuận chủ trương đầu tư;

b) Tham gia ý kiến về trường hợp thu hồi đất, phương án bồi thường, hỗ trợ, tái định cư, trường hợp cưỡng chế khi thực hiện thu hồi đất;

c) Tham gia ý kiến, giám sát quá trình xây dựng bảng giá đất và thực hiện bảng giá đất;

d) Tham gia hòa giải tranh chấp đất đai theo quy định của pháp luật;

đ) Giám sát việc thực hiện chính sách, pháp luật về thu hồi đất, trưng dụng đất; về bồi thường, hỗ trợ, tái định cư; về giao đất, cho thuê đất, chuyển mục đích sử dụng đất; về cấp Giấy chứng nhận quyền sử dụng đất, quyền sở hữu tài sản gắn liền với đất.

3. Mặt trận Tổ quốc Việt Nam, các tổ chức thành viên của Mặt trận có trách nhiệm trong việc tuyên truyền, phổ biến chính sách, pháp luật về đất đai tới Nhân dân, vận động Nhân dân thực hiện và chấp hành tốt chính sách, pháp luật về đất đai.',N'Ban CSLP TW Hội', 1)


select * from Content

insert into Account(iRoleID,sName,sEmail,sPassword,sPhone,dBirthofdate)
values
(1,N'Quản trị',N'quantri@traitimtinhtuong.vn',N'admin',N'0962948340','2002-08-31'),
(2,N'Hội viên',N'hoivien@traitimtinhtuong.vn',N'hoivien',N'0962948304','2002-08-31')

select * from Content