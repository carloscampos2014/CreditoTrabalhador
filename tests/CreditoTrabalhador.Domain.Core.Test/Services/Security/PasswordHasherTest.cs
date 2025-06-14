using CreditoTrabalhador.Domain.Core.Services.Security;
using FluentAssertions;

namespace CreditoTrabalhador.Domain.Core.Test.Services.Security;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher;

    public PasswordHasherTests()
    {
        _passwordHasher = new PasswordHasher();
    }

    // --- Testes para o método Generate ---

    // Este teste agora cobre null, empty e whitespace, pois Generate lança ArgumentException para todos eles.
    [Theory]
    [InlineData(null)]          // Senha nula
    [InlineData("")]            // Senha vazia
    [InlineData("   ")]         // Senha com apenas espaços em branco
    public void Generate_GivenNullEmptyOrWhitespacePassword_ThrowsArgumentException(string password)
    {
        // Arrange - password é fornecido pelo InlineData

        // Act
        Action act = () => _passwordHasher.Generate(password);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithParameterName("password")
           .WithMessage("A senha não pode ser vazia ou consistir apenas em espaços em branco.*"); // Verifica a mensagem exata ou parte dela
    }

    [Fact]
    public void Generate_GivenValidPassword_ReturnsNonNullOrEmptyHash()
    {
        // Arrange
        string password = "minhasenhateste123";

        // Act
        string hashedPassword = _passwordHasher.Generate(password);

        // Assert
        hashedPassword.Should().NotBeNullOrWhiteSpace();
        hashedPassword.Should().StartWithEquivalentOf("$2");
    }

    [Fact]
    public void Generate_GivenDifferentPasswords_ReturnsDifferentHashes()
    {
        // Arrange
        string password1 = "senhaforte123";
        string password2 = "outrasenha456";

        // Act
        string hash1 = _passwordHasher.Generate(password1);
        string hash2 = _passwordHasher.Generate(password2);

        // Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void Generate_GivenSamePasswordMultipleTimes_ReturnsDifferentHashesDueToSalt()
    {
        // Arrange
        string password = "senharepetida";

        // Act
        string hash1 = _passwordHasher.Generate(password);
        string hash2 = _passwordHasher.Generate(password);

        // Assert
        hash1.Should().NotBe(hash2);
    }


    // --- Testes para o método Verify ---

    // Este teste agora cobre null, empty e whitespace para 'password'.
    [Theory]
    [InlineData(null, "valid_hash_placeholder")]        // Senha plaintext nula
    [InlineData("", "valid_hash_placeholder")]          // Senha plaintext vazia
    [InlineData("   ", "valid_hash_placeholder")]       // Senha plaintext com apenas espaços em branco
    public void Verify_GivenNullEmptyOrWhitespacePassword_ThrowsArgumentException(string password, string placeholderHash)
    {
        // Arrange
        // Geramos um hash válido de uma senha real para o placeholder, pois precisamos de um hash real para o segundo parâmetro
        string hashedPassword = _passwordHasher.Generate("any_real_password_for_hash");

        // Act
        Action act = () => _passwordHasher.Verify(password, hashedPassword);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithParameterName("password")
           .WithMessage("A senha não pode ser vazia ou consistir apenas em espaços em branco.*");
    }

    // Este teste agora cobre null, empty e whitespace para 'hashedPassword'.
    [Theory]
    [InlineData("valid_password_placeholder", null)]           // Hash nulo
    [InlineData("valid_password_placeholder", "")]             // Hash vazio
    [InlineData("valid_password_placeholder", "   ")]          // Hash com apenas espaços em branco
    public void Verify_GivenNullEmptyOrWhitespaceHashedPassword_ThrowsArgumentException(string placeholderPassword, string hashedPassword)
    {
        // Arrange
        // A senha plaintext real não importa para este teste, usamos um placeholder
        string password = "any_real_password_for_check";

        // Act
        Action act = () => _passwordHasher.Verify(password, hashedPassword);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithParameterName("hashedPassword")
           .WithMessage("O hash da senha não pode ser vazio ou consistir apenas em espaços em branco.*");
    }

    [Theory]
    [InlineData("senha_secreta_123")]
    [InlineData("minha_senha_complexa!@#")]
    [InlineData("123456")]
    public void Verify_GivenCorrectPasswordAndHash_ReturnsTrue(string password)
    {
        // Arrange
        string hashedPassword = _passwordHasher.Generate(password);

        // Act
        bool result = _passwordHasher.Verify(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("senha_secreta_123", "senha_incorreta_123")]
    [InlineData("minha_senha_complexa!@#", "minha_senha_comlexa!@#")]
    [InlineData("123456", "654321")]
    public void Verify_GivenIncorrectPasswordAndHash_ReturnsFalse(string correctPassword, string incorrectPassword)
    {
        // Arrange
        string hashedPassword = _passwordHasher.Generate(correctPassword);

        // Act
        bool result = _passwordHasher.Verify(incorrectPassword, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Verify_GivenInvalidHashedPasswordFormat_ThrowsArgumentException()
    {
        // Arrange
        string password = "test_password";
        // Um hash que não segue o formato BCrypt
        string invalidHashedPassword = "not_a_valid_bcrypt_hash";

        // Act
        Action act = () => _passwordHasher.Verify(password, invalidHashedPassword);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithParameterName("hashedPassword")
           .WithMessage("O hash da senha fornecido é inválido ou malformado.*");
    }
}
