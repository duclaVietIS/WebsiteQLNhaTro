# Website Quản Lý Nhà Trọ

## 📝 Mô tả
Dự án ASP.NET Core quản lý nhà trọ, hỗ trợ các chức năng RESTful API, xác thực JWT, quản lý người dùng, phòng, hợp đồng, thanh toán, dịch vụ, báo cáo, tích hợp MySQL, upload ảnh toà nhà.

---
## 🚀 Chức năng chính

### 1. Quản lý toà nhà (Apartment)
#### Các chức năng:
- [x] Hiển thị danh sách, phân trang, tìm kiếm theo tên/địa chỉ
- [x] Thêm, sửa, xoá toà nhà
- [x] Upload ảnh toà nhà (tùy chọn)
#### Các task con:
- **Thiết kế DTO và model Apartment**
	- [x] Tạo DTO cho thêm/sửa/xoá, response, và model Apartment với các trường: tên, địa chỉ, ảnh (optional). Validate tên và địa chỉ là bắt buộc.
- **Tạo ApartmentController RESTful**
	- [x] Tạo controller với các API: lấy danh sách (có phân trang, search), thêm, sửa, xoá apartment. Sử dụng DTO đã thiết kế.
- **Xử lý lưu ảnh toà nhà bằng file storage**
	- [x] Cấu hình lưu file ảnh khi upload, trả về link ảnh. Cho phép thêm/sửa apartment không upload ảnh.
- **Validate dữ liệu đầu vào**
	- [x] Kiểm tra tên và địa chỉ là bắt buộc khi thêm/sửa. Trả về lỗi nếu thiếu thông tin.
- **Tích hợp phân trang và search**
	- [x] API lấy danh sách apartment hỗ trợ phân trang, tìm kiếm theo tên và địa chỉ.

### 2. Quản lý người dùng, xác thực JWT
- [x] Đăng ký, đăng nhập, xác thực JWT
- [x] Quản lý thông tin người dùng

### 3. Quản lý phòng, hợp đồng, thanh toán, dịch vụ
- [x] Quản lý phòng, hợp đồng thuê, thanh toán, dịch vụ đi kèm

### 4. Báo cáo, thống kê
- [x] Xuất báo cáo, thống kê hoạt động nhà trọ

---
## ⚡ Hướng dẫn chạy dự án
1. Clone repo:
	```bash
	git clone https://github.com/duclaVietIS/WebsiteQLNhaTro.git
	```
2. Cài đặt .NET 8 SDK, MySQL
5. Truy cập Swagger UI tại: `http://localhost:5000/swagger`

---
## 📁 Cấu trúc thư mục
```
WebsiteQLNhaTro/
├── Controllers/
├── DTOs/
├── Entities/
---

## 📚 Tài liệu tham khảo
- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Swagger](https://swagger.io/)

---
Yêu cầu: Xây dựng website quản lý nhà trọ với các chức năng:				
Cơ bản				
	1. Đăng ký, xác thực tài khoản qua email, quên mật khẩu			
		Các thông tin cần có của người dùng (users): name, email		
		Validate: name và email là bắt buộc, email phải đúng format mặc định của laravel, email không được trùng với user đã có		
	# Website Quản Lý Nhà Trọ
		Có thể hiển thị danh sách các toà nhà, thêm/sửa/xoá toà nhà		
		Các thông tin của toà nhà (apartments): tên, địa chỉ, ảnh toà nhà (optional)		
		Có thể hiển thị phân trang, search theo tên, địa chỉ		
		Validate: tên và địa chỉ là bắt buộc		
		Sử dụng file storage để lưu ảnh, có thể không upload ảnh toà nhà		
		Có thể hiển thị danh sách phòng trọ của mỗi tòa nhà, thêm/sửa/xoá phòng trọ		
		Các thông tin của phòng trọ (apartment_rooms): số phòng, giá thuê, số người đang ở, ảnh phòng trọ (optional)		
		Có thể hiển thị phân trang, search theo toà nhà, số phòng		
	4. Quản lý tiền trọ hàng tháng			
		Với mỗi phòng trọ, mỗi tháng user có thể nhập các thông tin:		
			Số điện sử dụng (có thể cho phép upload ảnh đồng hồ)	
			Số nước sử dụng (có thể cho phép upload ảnh đồng hồ)	
			Tổng số tiền cần thanh toán	
			Tổng số tiền đã thanh toán	
			Ngày thanh toán	
		Validate: số điện sử dụng, số nước sử dụng, tổng số tiền đã thanh toán là bắt buộc		
		Có thể edit lại thông tin của từng tháng cho từng phòng		
		Sử dụng file storage để lưu ảnh, có thể không upload ảnh đồng hồ		
	5. Thống kê			
		Vào ngày 10 hàng tháng, lấy các phòng mà tháng trước chưa thanh toán đủ tiền và gửi mail thông báo		
		Có màn hình để hiển thị số lượng và danh sách phòng chưa thanh toán đủ của tháng trước		
	6. Log			
		Với các thao tác dưới đây, khi thực hiện thì log lại vào database:		
			+ Tạo toà nhà	
			+ Tạo phòng trọ	
			+ Thực hiện thống kê và gửi mail thông báo chưa thanh toán đủ tiền	
				
Nâng cao				
	7. Quản lý hợp đồng thuê phòng (tenant_contracts)			
		Với mỗi phòng trọ thì có thể tạo hợp đồng thuê cho người thuê.		
		Khi nhập thông tin người thuê cho hợp đồng, có thể lựa chọn tạo người thuê mới hoặc chọn lại 1 trong các người thuê mà đã thuê trọ của user trước đây.		
		Khi tạo hợp đồng, nếu phòng đang có người thuê thì không thể tạo.		
		Thông tin người thuê bao gồm tên, số điện thoại, email		
		Thông tin hợp đồng bao gồm:		
			Kỳ hạn thanh toán (1 tháng, 3 tháng, 6 tháng, 1 năm)	
			Giá thuê	
			Cách trả tiền điện:	
			Giá điện	
			Số điện ban đầu	
				Trả theo đầu người (ví dụ 80k 1 người)
				Trả cố định theo phòng (ví dụ 200k 1 phòng)
				Trả theo số nước sử dụng (ví dụ 4k 1 số nước)
			Số người ở	
			Ghi chú	
	8. Bổ dung cho chức năng quản lý tiền trọ			
		Lưu thêm thông tin hợp đồng hiện tại đang thuê phòng vào thông tin tiền trọ hàng tháng		
		Nếu phòng chưa có người thuê thì không thể tạo tiền trọ hàng tháng		
	9. Bổ sung chức năng thống kê			
		Hiển thị biểu đồ tiền trọ và dư nợ theo tháng		
	10. Admin			
		Quản lý user		
		Hiển thị danh sách user có phân trang		
		Hiển thị số lượng toà nhà của mỗi user		
		Hiển thị số lượng phòng của mỗi user		
				
		Quản lý danh mục phụ phí hàng tháng (monthly_costs)		
		Admin có thể tạo sẵn list các danh mục phụ phí hàng tháng, ví dụ như tiền rác, tiền mạng, tiền điện dùng chung,...		
				
		Thống kê		
		Hiển thị biểu đồ tiền trọ và dư nợ theo tháng của tất cả các user		
	11. Bổ sung chức năng quản lý hợp đồng 			
		Khi tạo hợp đồng thì có thể chọn thêm các phụ phí khác từ list các danh mục phụ phí hàng tháng mà admin đã tạo		
		Mỗi loại phụ phí có thể lựa chọn cách tính tiền là: Trả theo đầu người, Trả theo phòng		
		Mỗi loại phụ phí có thể setting giá tiền		
				
				
Tham khảo cấu trúc DB				
