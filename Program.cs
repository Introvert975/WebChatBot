var builder = WebApplication.CreateBuilder(args);

// ���������� ����� � ���������.
builder.Services.AddRazorPages(); // ���������� ��������� Razor Pages.

builder.Services.AddSession(); // ���������� ��������� ������.

builder.Services.AddAuthentication("CookieAuth") // ��������� �������������� � �������������� cookie.
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "UserLoginCookie"; // ��� cookie ��� ��������������.
        config.LoginPath = "/Login"; // ���� ��� �������� ������.
    });

var app = builder.Build();

// ��������� ��������� ��������� HTTP-��������.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // ��������� ����������� ������ ��� �������� �����.
    app.UseHsts(); // ��������� HSTS (HTTP Strict Transport Security).
}

app.UseHttpsRedirection(); // ��������������� HTTP-�������� �� HTTPS.
app.UseStaticFiles(); // ��������� ��������� ����������� ������.

app.UseRouting(); // ��������� �������������.

app.UseAuthentication(); // ��������� ��������������.

app.UseSession(); // ��������� ��������� ������.

app.MapRazorPages(); // ��������� ��������� ��� Razor Pages.

app.Run(); // ������ ����������.