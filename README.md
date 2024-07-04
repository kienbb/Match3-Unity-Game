# Lời Giải Cho Bài Test Unity

## 1. Cải Thiện Hiệu Suất Game
Ba thay đổi có thể cải thiện hiệu suất game:
- **Thêm Sprite Atlas**: Giảm draw call (đặt trong thư mục `Assets/SpriteAtlas`).
- **Thêm Class ResourceManager**: Cache các resource đã load khi sử dụng `Resource.Load`, tránh việc phải load lại.
- **Thêm Class ObjectPoolManager**: Thay thế hàm `Instantiate` và `Destroy` khi spawn board, cell, tránh việc phân bổ thêm RAM khi tạo mới một GameObject.

## 2. Sử Dụng ScriptableObject để Skin Các Cell
- Thêm ScriptableObject `Skin` trong thư mục `Assets/Resource` để chứa cấu hình của các skin. Chỉ cần chọn index của skin muốn sử dụng trước khi chơi game.
- Đang sử dụng `OnValidate` để đổi sprite, cần chỉnh sửa thêm để dùng trên sản phẩm thực tế.

## 3. Thêm Nút Restart
- Nút Restart sẽ cho phép chơi lại màn đang chơi, với board được reset và spawn mới.

## 4. Chỉnh Sửa Hàm FillGapsWithNewItems
Thêm hàm `GetDesireType`:
```csharp
private NormalItem.eNormalType GetDesireType(Cell cell)
{
    // Lấy ra danh sách các type xuất hiện ít nhất theo thứ tự tăng dần
    Dictionary<NormalItem.eNormalType, int> dict = new Dictionary<NormalItem.eNormalType, int>();
    for (int x = 0; x < boardSizeX; x++)
    {
        for (int y = 0; y < boardSizeY; y++)
        {
            Cell aCell = m_cells[x, y];
            if (aCell.IsEmpty) continue;

            NormalItem item = aCell.Item as NormalItem;
            if (item == null) continue;

            if (dict.ContainsKey(item.ItemType))
            {
                dict[item.ItemType]++;
            }
            else
            {
                dict.Add(item.ItemType, 1);
            }
        }
    }
    List<NormalItem.eNormalType> leastTypes = dict.OrderBy(x => x.Value).Select(x => x.Key).ToList();

    // Bỏ đi các type đã xuất hiện xung quanh
    if (cell.NeighbourUp != null && !cell.NeighbourUp.IsEmpty && cell.NeighbourUp.Item is NormalItem)
        RemoveType((cell.NeighbourUp.Item as NormalItem).ItemType);
    if (cell.NeighbourRight != null && !cell.NeighbourRight.IsEmpty && cell.NeighbourRight.Item is NormalItem)
        RemoveType((cell.NeighbourRight.Item as NormalItem).ItemType);
    if (cell.NeighbourBottom != null && !cell.NeighbourBottom.IsEmpty && cell.NeighbourBottom.Item is NormalItem)
        RemoveType((cell.NeighbourBottom.Item as NormalItem).ItemType);
    if (cell.NeighbourLeft != null && !cell.NeighbourLeft.IsEmpty && cell.NeighbourLeft.Item is NormalItem)
        RemoveType((cell.NeighbourLeft.Item as NormalItem).ItemType);

    // Trả về type ít xuất hiện nhất
    return leastTypes[0];

    void RemoveType(NormalItem.eNormalType type)
    {
        if (leastTypes.Contains(type))
        {
            leastTypes.Remove(type);
        }
    }
}
```

## 5. Góp Ý
- Base game đang ở mức đơn giản, dễ sửa và thêm bớt tính năng.
- Thi thoảng không move tính điểm được.
- Nên đặt tên các type, prefab, và sprite tương ứng một cách đồng bộ để dễ nhớ (ví dụ: `Constants.PREFAB_NORMAL_TYPE_ONE`, `itemNormal01`, `NormalItem.eNormalType.TYPE_ONE`).
- Có 2 thư mục Script `Utilities` và `Utility`?
