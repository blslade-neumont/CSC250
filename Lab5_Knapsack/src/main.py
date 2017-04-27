

def main():
    path = input('Enter a path. (sample0.txt) ') or 'sample0.txt'
    maxWeight = 0
    maxItems = 0
    items = None
    
    try:
        with open(path) as file:
            lines = [line for line in file if line.strip() != '' and not line.strip().startswith('//')]
            maxWeight = int(lines[0])
            maxItems = int(lines[1])
            items = [tuple(map(str.strip, line.split(','))) for line in lines[2:]]
    except:
        print('An Error occurred while reading input file.')
        return
    
    print('maxWeight', maxWeight)
    print('maxItems', maxItems)
    for item in items:
        print(item)

if __name__ == '__main__':
    main()
