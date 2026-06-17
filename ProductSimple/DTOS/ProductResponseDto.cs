namespace ProductSimple.DTOs;

public record ProductResponseDto(int Id, string Name, decimal? Price, string Description, DateTime? CreatedAt);
