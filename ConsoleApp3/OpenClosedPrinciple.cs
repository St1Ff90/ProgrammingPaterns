using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public enum Size
    {
        small,
        medium,
        large
    }

    public enum Color
    {
        green,
        blue,
        red
    }

    public class Product
    {
        public readonly string Name;
        public readonly Size Size;
        public readonly Color Color;

        public Product(string name, Size size, Color color)
        {
            Name = name;
            Size = size;
            Color = color;
        }
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Specification<T> spec);
    }

    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T item);

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new AndSpecifications<T>(left, right);
        }
    }

    public class ColorSpecification : Specification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public override bool IsSatisfied(Product item)
        {
            if (item.Color == _color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SizeSpecification : Specification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public override bool IsSatisfied(Product item)
        {
            return item.Size == _size;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, Specification<Product> spec)
        {
            foreach (Product item in items)
            {
                if (spec.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }

    //combinator
    public class AndSpecifications<T> : Specification<T>
    {
        private readonly Specification<T> _specification1;
        private readonly Specification<T> _specification2;

        public AndSpecifications(Specification<T> specification1, Specification<T> specification2)
        {
            _specification1 = specification1;
            _specification2 = specification2;
        }

        public override bool IsSatisfied(T item)
        {
            return _specification1.IsSatisfied(item) && _specification2.IsSatisfied(item);
        }
    }
}
